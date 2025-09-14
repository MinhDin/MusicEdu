using System;
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
		public int      Index { get; private set; }
		
		bool _isHit;

		void OnEnable()
		{
			GameEvents.OnNoteCorrect += OnNoteCorrect;
		}

		void OnDisable()
		{
			GameEvents.OnNoteCorrect -= OnNoteCorrect;
		}

		void OnNoteCorrect(int index, NoteData obj)
		{
			if (Index == index)
			{
				SetHit(true);
				RefreshDisplay();
			}
		}

		public void SetNote(NoteData note, int index)
		{
			NoteData = note;
			Index    = index;
			SetHit(false);
			RefreshDisplay();
		}

		public void SetHit(bool isHit = true)
		{
			_isHit = isHit;
		}
		
		public void RefreshDisplay()
		{
			var theme = GameEvents.GetTheme?.Invoke(NoteData.BaseId) ?? default;
			MainSpr.color      = theme.MainColor;
			NoteCodeTxt.text   = NoteData.ToText();
			HighlightSpr.color = GetSaturatedColor(theme.MainColor);
			MainSpr.size       = new Vector2(NoteData.Duration - 0.05f, MainSpr.size.y);

			var alpha = _isHit ? 0.5f : 1;
			HighlightSpr.color = new Color(HighlightSpr.color.r, HighlightSpr.color.g, HighlightSpr.color.b, alpha);
			MainSpr.color      = new Color(MainSpr.color.r, MainSpr.color.g, MainSpr.color.b, alpha);
		}
		
		private Color GetSaturatedColor(Color color)
		{
			float h, s, v;
			Color.RGBToHSV(color, out h, out s, out v);
			return Color.HSVToRGB(h, s, Mathf.Max(v * 0.7f, 0f));
		}
	}
}
