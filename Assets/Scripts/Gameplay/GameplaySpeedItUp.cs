using Core;
using UnityEngine;

namespace Core
{
	public class GameplaySpeedItUp : GameplayBase
	{
		protected int _currentStreak;
		
		public override void InitLesson(JsonObject modeConfig, SongData song, GeneralConfig config)
		{
			base.InitLesson(song, config);
			_currentStreak           =  0;
			GameEvents.OnNoteCorrect += OnNoteCorrect;
			GameEvents.OnNoteMiss    += OnNoteMiss;
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			
		}
		
		void OnNoteCorrect(NoteData note)
		{
			if (_currentStreak < 0)
			{
				_currentStreak = 1;
			}
			else
			{
				_currentStreak++;
				if (_currentStreak >= 4)
				{
					GameEvents.RequestSetOffsetBPM?.Invoke(10 * Mathf.FloorToInt(_currentStreak / 4));
				}
			}
		}
		
		void OnNoteMiss(NoteData note)
		{
			if (_currentStreak >= 0)
			{
				_currentStreak = -1;
			}
			else
			{
				_currentStreak--;
				if (_currentStreak <= -3)
				{
					GameEvents.RequestSetOffsetBPM?.Invoke(-10 * Mathf.FloorToInt(-_currentStreak / 3));
				}
			}
		}
	}

	
}
