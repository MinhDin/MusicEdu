using Core;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Core
{ 
	/// <summary>
	/// Gameplay mode that adjusts the song's BPM based on player performance.
	/// Increases tempo when hitting correct notes in streak and decreases when missing notes.
	/// </summary>
	public class GameplaySpeedItUp : GameplayBase
	{
		protected int             _currentStreak;
		protected SpeedItUpConfig _modeConfig;
		
		public override void InitLesson(JObject modeConfig, SongData song, GeneralConfig config)
		{
			base.InitLesson(modeConfig, song, config);
			_modeConfig = new();
			if(modeConfig != null) _modeConfig.Override(modeConfig);
			
			_currentStreak           =  0;
			GameEvents.OnNoteCorrect += OnNoteCorrect;
			GameEvents.OnNoteMiss    += OnNoteMiss;
			GameEvents.RequestMusic(false);
			
			GenerateLookingForNote();
		}

		public override void LessonEnd()
		{
			GameEvents.OnNoteCorrect -= OnNoteCorrect;
			GameEvents.OnNoteMiss    -= OnNoteMiss;
		}
		
		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}

		void OnNoteCorrect(int index, NoteData note)
		{
			_currentStreak++;
			GameEvents.RequestSetOffsetBPM?.Invoke(_modeConfig.BPMChange * Mathf.FloorToInt(_currentStreak / _modeConfig.SuccessStreak));
		}

		void OnNoteMiss(int index, NoteData note)
		{
			_currentStreak--;
			GameEvents.RequestSetOffsetBPM?.Invoke(-_modeConfig.BPMChange * Mathf.FloorToInt(-_currentStreak / _modeConfig.FailStreak));
		}
	}
	
	/// <summary>
	/// Configuration parameters for SpeedItUp gameplay mode including BPM change rates and streak thresholds
	/// </summary>
	public class SpeedItUpConfig
	{
		public float BPMChange = 10;
		public int SuccessStreak = 4;
		public int FailStreak = 3;

		public void Override(JObject config)
		{
			if(config.ContainsKey("BPMChange")) BPMChange = float.Parse(config.GetValue("BPMChange").ToString());
			if(config.ContainsKey("SuccessStreak")) SuccessStreak = int.Parse(config.GetValue("SuccessStreak").ToString());
			if(config.ContainsKey("FailStreak")) FailStreak = int.Parse(config.GetValue("FailStreak").ToString());
		}
	}

	
}
