using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class SoundInstrumentManager : MonoBehaviour
	{
		static string DefaultInstrument = "ClassicPiano";
		
		public InstrumentConfig DefaultPianoConfig;

		InstrumentConfig _currentInstrument;

		Dictionary<int, AudioInstrumentNote> _instrumentNotes = new();
		Dictionary<string, InstrumentConfig> _instrumentConfigs = new();
		
		public void Init()
		{
			GameEvents.GetInstrumentNote = GetInstrumentNote;
			_instrumentConfigs.Add(DefaultInstrument, DefaultPianoConfig);
			LoadInstrument(DefaultInstrument);
			
			GameEvents.RequestLoadAudioInstrument = LoadInstrument;
		}

		AudioInstrumentNote GetInstrumentNote(int baseNoteId) => _instrumentNotes.GetValueOrDefault(baseNoteId);
		
		public void LoadInstrument(string instrumentId)
		{
			_currentInstrument = _instrumentConfigs.GetValueOrDefault(instrumentId, DefaultPianoConfig);
			_instrumentNotes.Clear();

			if(_currentInstrument == null) return;

			foreach(var note in _currentInstrument.Notes)
			{
				_instrumentNotes[note.BaseNoteId] = note;
			}
		}
	}
}
