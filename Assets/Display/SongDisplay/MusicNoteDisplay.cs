using TMPro;
using UnityEngine;

namespace Core
{
	public class MusicNoteDisplay : MonoBehaviour
	{
		public SpriteRenderer MainSpr;
		public SpriteRenderer HighlightSpr;
		public TextMeshPro    NoteCodeTxt;

		public NoteData NoteData { get; private set; }

		public void SetNote(NoteData note)
		{
			NoteData = note;
			RefreshDisplay();
		}

		public void RefreshDisplay()
		{
			var theme = GameEvents.GetTheme?.Invoke(NoteData.BaseId) ?? default;
			MainSpr.color      = theme.MainColor;
			NoteCodeTxt.text   = NoteData.ToText();
			HighlightSpr.color = GetSaturatedColor(theme.MainColor);
			MainSpr.size       = new Vector2(NoteData.Duration - 0.05f, MainSpr.size.y);
		}
		
		private Color GetSaturatedColor(Color color)
		{
			float h, s, v;
			Color.RGBToHSV(color, out h, out s, out v);
			return Color.HSVToRGB(h, s, Mathf.Max(v * 0.7f, 0f));
		}
	}
}
