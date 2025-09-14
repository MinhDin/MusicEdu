using UnityEngine;

namespace Core
{
	public static class Note
	{
		public static int NumberOfNote = 7;
		
		public static string ToText(this NoteData note) => $"{note.BaseId.ToText()}{(note.Acc == Accidentals.Natural ? string.Empty : note.Acc.ToText())}";

		public static NoteChar ToNoteChar(this int baseNoteId) =>
			baseNoteId <= 0 
				? NoteChar.O 
				: ((baseNoteId - 1) % NumberOfNote) switch
				{
					0 => NoteChar.A,
					1 => NoteChar.B, 
					2 => NoteChar.C,
					3 => NoteChar.D,
					4 => NoteChar.E,
					5 => NoteChar.F,
					6 => NoteChar.G,
					_ => NoteChar.A,
				};
		
		public static char ToText(this int baseNoteId) =>
			((baseNoteId - 1) % NumberOfNote) switch
			{
				0 => 'A',
				1 => 'B',
				2 => 'C',
				3 => 'D',
				4 => 'E',
				5 => 'F',
				6 => 'G',
				_ => 'A',
			};
		
		public static char ToText(this Accidentals acc) =>
			acc switch
			{
				Accidentals.Natural => '♮',
				Accidentals.Flat => '♭',
				Accidentals.Sharp => '♯',
				_ => '♮',
			};
		
	}

	[System.Serializable]
	public struct NoteData
	{
		public int         BaseId; // A1=1,B1=2,A3=22,E3=26,A4=29,B4=30
		public Accidentals Acc;
		public float       Duration; // 1 = whole note
	}

	public enum Accidentals
	{
		Natural,
		Flat,
		Sharp
	}

	[System.Serializable]
	public struct NoteTheme
	{
		public Color MainColor;
	}

	public enum NoteChar
	{
		O, A, B, C, D, E, F, G // O = Rest
	}
}
