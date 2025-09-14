using System.Collections.Generic;
using PathHeroes;
using UnityEngine;

namespace Core
{
	public class SongManager : MonoBehaviour
	{
		public List<SongData> DefaultSongs;

		List<SongData> Songs = new();
		
		public void Init()
		{
			Songs.AddRange(DefaultSongs);	
		}

		public void AddLoveStory()
		{
			Songs.Add(new SongData
			{
				Notes = new List<NoteData>
				{
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3 
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 20, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 20, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 20, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 22, Acc = Accidentals.Natural, Duration = 1.0f }, // G3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 17, Acc = Accidentals.Natural, Duration = 1.0f }, // D3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 0.5f }, // E3
					new NoteData { BaseId = 20, Acc = Accidentals.Natural, Duration = 0.5f }, // F3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
					new NoteData { BaseId = 19, Acc = Accidentals.Natural, Duration = 1.0f }, // E3
				}
			});
		}

	}
}
