using UnityEngine;
using System.Collections.Generic;
using Core;

namespace Core
{
	[System.Serializable]
	public class SongData
	{
		public List<NoteData> Notes;
		public AudioClip      Music;
		public float          MusicBPM;
	}
}
