using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	[CreateAssetMenu(fileName = "ThemeConfig", menuName = "MusicEdu/Theme Config")]
	public class ThemeConfig : ScriptableObject
	{
		[System.Serializable]
		public class ThemeSetting
		{
			public NoteChar  NodeChar;
			public NoteTheme NoteTheme;
		}

		public List<ThemeSetting> NoteThemes;
	}
}
