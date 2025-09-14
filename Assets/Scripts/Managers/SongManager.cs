using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class SongManager : MonoBehaviour
	{
		public SongData DefaultSongs;

		Dictionary<string, SongData> Songs = new();
		
		public void Init()
		{
			AddLoveStory();
			AddOdeToJoy();

			GameEvents.GetAvailableSongs = () => Songs;
			GameEvents.GetSong           = songId => Songs.GetValueOrDefault(songId, DefaultSongs);  
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
				}
			});
		}

		public void AddOdeToJoy()
		{
			Songs.Add("OdeToJoy", new SongData
			{
				Notes = new List<NoteData>
				{
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 27, Acc = Accidentals.Natural, Duration = 1.0f }, // F3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 27, Acc = Accidentals.Natural, Duration = 1.0f }, // F3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 23, Acc = Accidentals.Natural, Duration = 1.0f }, // C3
					new NoteData { BaseId = 23, Acc = Accidentals.Natural, Duration = 1.0f }, // C3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 26, Acc = Accidentals.Natural, Duration = 1.5f }, // E3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 0.5f }, // D3
					new NoteData { BaseId = 24, Acc = Accidentals.Natural, Duration = 2.0f }, // D3
				}
			});
		}

	}
}
