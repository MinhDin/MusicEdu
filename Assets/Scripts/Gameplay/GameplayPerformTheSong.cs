using Core;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Core
{
	public class GameplayPerformTheSong : GameplayBase
	{
		int                  _currentStreak;
		PerformTheSongConfig _modeConfig;
		bool                 _isMusic;
		
		public override void InitLesson(JObject modeConfig, SongData song, GeneralConfig config)
		{
			base.InitLesson(modeConfig, song, config);
			_modeConfig = new();
			if(modeConfig != null) _modeConfig.Override(modeConfig);
			
			_currentStreak           =  0;
			
			GameEvents.OnNoteCorrect += OnNoteCorrect;
			GameEvents.OnNoteMiss    += OnNoteMiss;

			_isMusic = true; // Start with music
			GameEvents.RequestMusic?.Invoke(_isMusic); 
			
			GenerateLookingForNote();
		}

		public override void LessonEnd()
		{
			GameEvents.OnNoteCorrect -= OnNoteCorrect;
			GameEvents.OnNoteMiss    -= OnNoteMiss;
		}
		
		void OnNoteCorrect(int index, NoteData note)
		{
			if (_currentStreak < 0)
			{
				_currentStreak = 1;
			}
			else
			{
				_currentStreak++;
				if (_currentStreak >= _modeConfig.Streak && !_isMusic)
				{
					_isMusic = true;
					GameEvents.RequestMusic?.Invoke(_isMusic);
				}
			}
		}
		
		void OnNoteMiss(int index, NoteData note)
		{
			if (_currentStreak >= 0)
			{
				_currentStreak = -1;
			}
			else
			{
				_currentStreak--;
				if (_currentStreak <= -_modeConfig.Streak && _isMusic)
				{
					_isMusic = false;
					GameEvents.RequestMusic?.Invoke(_isMusic);
				}
			}
		}
	}
	
	public class PerformTheSongConfig
	{
		public int Streak = 3;

		public void Override(JObject config)
		{
			if (config.ContainsKey("Streak")) Streak = int.Parse(config.GetValue("Streak").ToString());
		}
	}
}
