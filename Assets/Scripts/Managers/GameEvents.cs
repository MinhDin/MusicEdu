using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public static class GameEvents
	{
		// Get something
		public static Func<int, AudioInstrumentNote>     GetInstrumentNote;
		public static Func<int, NoteTheme>               GetTheme;
		public static Func<Dictionary<string, SongData>> GetAvailableSongs;
		public static Func<string, SongData>             GetSong;
		public static Func<GeneralConfig>                GetGeneralConfig;
		public static Func<Type, string, MonoBehaviour>  GetDisplayObj;

		// Request a handler
		public static Action<string>       RequestLoadAudioInstrument;
		public static Action<float>        RequestSetOffsetBPM;
		public static Action<LessonResult> RequestEndLesson;
		
		// Input from user
		public static Func<List<(float, NoteData)>> GetCurrentPlayNotes;     // Time
		public static Action<int>                   RequestConsumePlayNotes; // Index
		public static Action<int, NoteData>         OnNoteCorrect;           // Index, data
		public static Action<int, NoteData>         OnNoteMiss;              // Index, data
		
	}
}
