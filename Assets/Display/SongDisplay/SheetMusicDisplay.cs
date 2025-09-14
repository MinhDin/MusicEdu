using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace Core
{
	public class SheetMusicDisplay : MonoBehaviour
	{
		public MusicNoteDisplay NotePrefab;
		public GameObject       SeparatorPrefab;
		public GameObject       Bird;
		public Transform        SheetPivot;
		public float            SheetYOffset = 0.2f;
		
		public float                  CurrentTime { get; private set; }
		public MusicNoteDisplay       CurrentNote { get; private set; }
		public List<MusicNoteDisplay> AllNotes => _notes;
		
		List<MusicNoteDisplay>       _notes      = new();
		List<GameObject>             _separators = new();
		ObjectPool<MusicNoteDisplay> _notePool;
		ObjectPool<GameObject>       _separatorPool;
		
		void Awake()
		{
			_separatorPool = new ObjectPool<GameObject>(
				createFunc: () => Instantiate(SeparatorPrefab, SheetPivot),
				actionOnGet: separator => separator.SetActive(true),
				actionOnRelease: separator => separator.SetActive(false),
				actionOnDestroy: Destroy
			);

			_notePool = new ObjectPool<MusicNoteDisplay>(
				createFunc: () => Instantiate(NotePrefab, SheetPivot),
				actionOnGet: note => note.gameObject.SetActive(true),
				actionOnRelease: note => note.gameObject.SetActive(false),
				actionOnDestroy: note => Destroy(note.gameObject)
			);
		}

		public void LoadSong(SongData songData, int octave = 3)
		{
			foreach (var note in _notes)
			{
				_notePool.Release(note);
			}

			foreach (var separator in _separators)
			{
				_separatorPool.Release(separator);
			}
			
			_notes.Clear();
			_separators.Clear();

			var minNote = octave * Note.NumberOfNote + 1 + 2; // +2 because start with C

			float duration      = 0;
			float totalDuration = 0;
			float seperatorPos  = 1;
			
			for (var i = 0; i < songData.Notes.Count; i++)
			{
				var noteData    = songData.Notes[i];
				var noteDisplay = _notePool.Get();
				noteDisplay.SetNote(noteData, i);
				var y = (noteData.BaseId - minNote) * SheetYOffset;
				noteDisplay.transform.localPosition = new Vector3(totalDuration, y, -1);
				_notes.Add(noteDisplay);

				totalDuration += noteData.Duration;
				duration      += noteData.Duration;
				
				if(duration >= 1.0f)
				{
					var separator = _separatorPool.Get();
					separator.transform.localPosition = new Vector3(seperatorPos, 0, 0);
					_separators.Add(separator);
					duration -= 1.0f;
					seperatorPos++;
				}
			}
		}
		
		public void UpdateSong(float noteAmount)
		{
			foreach (var note in _notes)
			{
				note.transform.Translate(Vector3.left * noteAmount);
			}

			foreach (var seperator in _separators)
			{
				seperator.transform.Translate(Vector3.left * noteAmount);
			}
			
			var input = GameEvents.GetCurrentPlayNotes();
			if (input != null && input.Count > 0)
			{
				var (_, noteData) = input[0];
				var minNote = 3 * Note.NumberOfNote + 1 + 2;
				var y = (noteData.BaseId - minNote) * SheetYOffset;
				Bird.transform.localPosition = new Vector3(Bird.transform.localPosition.x, y, Bird.transform.localPosition.z);
			}
		}

		void OnDisable()
		{
			foreach (var note in _notes)
			{
				_notePool.Release(note);
			}
			_notes.Clear();

			foreach (var separator in _separators)
			{
				_separatorPool.Release(separator);
			}
			_separators.Clear();
		}
	}
}
