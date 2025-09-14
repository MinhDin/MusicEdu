using System;
using System.Collections.Generic;
using PathHeroes;
using UnityEngine;

namespace Core
{
	public static class GameEvents
	{
		public static Func<string, AudioClip> GetAudioClip;
		public static Func<int, NoteTheme>    GetTheme;
		public static Func<List<SongData>>  GetAvailableSongs;
	}
}
