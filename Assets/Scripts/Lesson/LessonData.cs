using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core
{
	[System.Serializable]
	public class LessonData
	{
		public string  SongId;
		public string  GameplayMode;
		public JObject ModeConfig;
		public string  SheetMode;
		public string  InputMode;
		public string  Instrument;
		public float   HitWindow;
		public float   BPM;
		public int     StartInputNote;
		public int     EndInputNote;
	}
	
	
}
