using UnityEngine;
using System.Collections.Generic;

namespace Core
{
	// Only affect display like Color
	public class ThemeManager : MonoBehaviour
	{
		public ThemeConfig DefaultThemeConfig;
		Dictionary<NoteChar, NoteTheme> themeDictionary = new();

		public void Init()
		{
			GameEvents.GetTheme = (baseNoteId) => themeDictionary.TryGetValue(baseNoteId.ToNoteChar(), out var theme) ? theme : new NoteTheme();

			LoadTheme(DefaultThemeConfig);
		}
			
		public void LoadTheme(ThemeConfig config)
		{
			themeDictionary.Clear();
			foreach (var theme in config.NoteThemes)
			{
				themeDictionary[theme.NodeChar] = theme.NoteTheme;
			}
		}
	}
}
