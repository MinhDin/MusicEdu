using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Core
{
	/// <summary> Store songs </summary>
	public class SongManager : MonoBehaviour
	{
		public AudioClip          OdeToJoyMusic;
		public AudioMixerSnapshot MuteMusicSnapshot;
		public AudioMixerSnapshot NormalSnapshot;
		
		Dictionary<string, SongData> Songs = new();
		
		public void Init()
		{
			AddLoveStory();
			AddOdeToJoy();

			GameEvents.GetAvailableSongs = () => Songs;
			GameEvents.GetSong           = songId => Songs.GetValueOrDefault(songId);
			GameEvents.RequestMusic = isMusic =>
				{
					if (isMusic) NormalSnapshot.TransitionTo(1.5f);
					else MuteMusicSnapshot.TransitionTo(1.5f);
				};
		}
		
		public void AddLoveStory()
		{
			Songs.Add("LoveStory", new SongData
			{
				Notes = new List<NoteData>
				{
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3 
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 27, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 27, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 27, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 27, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
				},
				Music = OdeToJoyMusic,
				MusicBPM = 120.0f,
			});
		}

		public void AddOdeToJoy()
		{
			Songs.Add("OdeToJoy", new SongData
			{
				Notes = new List<NoteData>
				{
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.5f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 0.5f }, // B4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 2.0f }, // B4
					//new NoteData { BaseId = 1, Acc  = Accidentals.Natural, Duration = 1.5f }, // Rest
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.5f }, // B4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 0.5f }, // A4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 2.0f }  // A4
				},
				Music    = OdeToJoyMusic,
				MusicBPM = 120.0f,
			});
			
			Songs.Add("OdeToJoyRest", new SongData
			{
				Notes = new List<NoteData>
				{
					new NoteData { BaseId = 0, Acc  = Accidentals.Natural, Duration = 2.0f }, // Rest
					new NoteData { BaseId = 0, Acc  = Accidentals.Natural, Duration = 2.0f }, // Rest
					//new NoteData { BaseId = 1, Acc  = Accidentals.Natural, Duration = 2.0f }, // Rest
					new NoteData { BaseId = 0, Acc  = Accidentals.Natural, Duration = 2.0f }, // Rest
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.5f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 0.5f }, // B4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 2.0f }, // B4
					//new NoteData { BaseId = 1, Acc  = Accidentals.Natural, Duration = 1.5f }, // Rest
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 33, Acc = Accidentals.Natural, Duration = 1.0f }, // E4
					new NoteData { BaseId = 32, Acc = Accidentals.Natural, Duration = 1.0f }, // D4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 1.0f }, // A4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.0f }, // B4
					new NoteData { BaseId = 31, Acc = Accidentals.Natural, Duration = 1.0f }, // C4
					new NoteData { BaseId = 30, Acc = Accidentals.Natural, Duration = 1.5f }, // B4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 0.5f }, // A4
					new NoteData { BaseId = 29, Acc = Accidentals.Natural, Duration = 2.0f }  // A4
				},
				Music    = OdeToJoyMusic,
				MusicBPM = 100,
			});
		}

	}
}
