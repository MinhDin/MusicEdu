using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Core
{
	public class Lesson
	{
		public enum State
		{
			Pause,
			Playing,
		}
		
		public float TotalBPM => (_lessonData?.BPM ?? 150) + _offsetBPM;
		public float CurrentBPM => _currentBPM;
		
		LessonData        _lessonData;
		GameplayBase      _gameplay;
		State             _currentState;
		SheetMusicDisplay _sheet;
		float             _offsetBPM;
		float             _currentBPM;
		float             _currentBPMVec;
		
		public void LoadLesson(LessonData lessonData)
		{
			GameEvents.RequestSetOffsetBPM = bpm => _offsetBPM = bpm;
			_lessonData                    = lessonData;
			_offsetBPM                     = 0;
			// Song
			var song = GameEvents.GetSong?.Invoke(_lessonData.SongId);
			// Mode
			switch (_lessonData.GameplayMode)
			{
				case "SpeedItUp": _gameplay = new GameplaySpeedItUp(); break;
				case "Perform":   _gameplay = new GameplayPerformTheSong(); break;
				default:          return;
			}
			// Input
			var input = GameEvents.GetDisplayObj?.Invoke(typeof(InputInstrumentBase), _lessonData.InputMode) as InputInstrumentBase;
			// Sheet
			_sheet = GameEvents.GetDisplayObj?.Invoke(typeof(SheetMusicDisplay), _lessonData.SheetMode) as SheetMusicDisplay;
			// Sound
			GameEvents.RequestLoadAudioInstrument?.Invoke(_lessonData.Instrument);
			// Config
			var config = GameEvents.GetGeneralConfig?.Invoke();

			if (song == null || input == null || config == null || _sheet == null)
			{
				Debug.LogError($"Can't load lesson {song}:{input}:{config}:{_sheet}");
				return;
			}

			config.HitWindow = lessonData.HitWindow;
			input.LoadRange(lessonData.StartInputNote, lessonData.EndInputNote);
			_sheet.LoadSong(song);
			_currentState = State.Pause;
			_currentBPM = lessonData.BPM;
			_gameplay.InitLesson(lessonData.ModeConfig, song, config);
		}

		public void Update(float deltaTime)
		{
			var input = GameEvents.GetCurrentPlayNotes?.Invoke();
			if (input == null) return;

			_currentBPM = Mathf.SmoothDamp(_currentBPM, TotalBPM, ref _currentBPMVec, 0.3f);
			_sheet.UpdateBPM(_currentBPM);
			// 1 note move equal a full note
			var noteTime = _currentBPM / 60.0f * Time.deltaTime;
			
			if (_currentState == State.Playing)
			{
				_sheet.UpdateSong(noteTime);
				_gameplay.Update(noteTime);
			}
			else if (_currentState == State.Pause)
			{
				if (input.Count > 0)
				{
					_currentState = State.Playing;
					_sheet.StartSong();
					GameEvents.OnLessonStart?.Invoke();
				}
			}
		}

		public void LessonEnd()
		{
			_sheet?.EndSong();
			_gameplay?.LessonEnd();
		}
	}
	
	[System.Serializable]
	public class LessonResult
	{
		public int  note_hit;
		public int  note_miss;
		public bool song_end;
	}
}
