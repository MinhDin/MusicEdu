using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	[CreateAssetMenu(fileName = "InstrumentConfig", menuName = "MusicEdu/Instrument Config")]
	public class InstrumentConfig : ScriptableObject
	{
		public List<AudioInstrumentNote> Notes;
	}
	
	[System.Serializable]
	public struct AudioInstrumentNote
	{
		public int       BaseNoteId;
		public AudioClip Clip;
	}
}
