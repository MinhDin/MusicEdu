using System;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;

namespace Core
{
	public class PianoKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public Action<PianoKey> OnPlayNote;
		
		public AudioSource AudioSource;
		public GameObject  NormalState;
		public GameObject  HoldState;
		
		public List<SpriteRenderer> ColorSprs;
		public List<TextMeshPro>    NoteNameTxts;

		public enum KeyState
		{
			Up,
			Down,
			Hold
		}

		public KeyState CurrentState { get; private set; } = KeyState.Up;
		public PianoKeyData Data { get; private set; }
		
		public void SetData(PianoKeyData data)
		{
			Data = data;
			RefreshDisplay();
		}
		
		private void RefreshDisplay()
		{
			switch (CurrentState)
			{
				case KeyState.Up:
					NormalState.SetActive(true);
					HoldState.SetActive(false);
					break;
				case KeyState.Down:
					NormalState.SetActive(false);
					HoldState.SetActive(true);
					break;
				case KeyState.Hold:
					NormalState.SetActive(false);
					HoldState.SetActive(true);
					break;
			}

			if (Data == null) return;
			
			var theme = GameEvents.GetTheme?.Invoke(Data.Note.BaseId) ?? default;

			foreach (var sprRender in ColorSprs) sprRender.color = theme.MainColor;
			foreach (var txt in NoteNameTxts)
			{
				txt.text  = Data.KeyText;
				txt.color = theme.MainColor;
			}
			
			AudioSource.clip = Data.AudioClip;
		}
		
		private Color GetDarkerColor(Color color)
		{
			float h, s, v;
			Color.RGBToHSV(color, out h, out s, out v);
			return Color.HSVToRGB(h, s, Mathf.Max(v * 0.7f, 0f));
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			CurrentState = KeyState.Down;
			RefreshDisplay();
			AudioSource.Play();
			Debug.Log($"Play note {Data.Note.BaseId}");
			OnPlayNote?.Invoke(this);	
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			CurrentState = KeyState.Up;
			RefreshDisplay();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				CurrentState = KeyState.Hold;
				RefreshDisplay();
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (CurrentState != KeyState.Up)
			{
				CurrentState = KeyState.Up;
				RefreshDisplay();
			}
		}
	}

	[System.Serializable]
	public class PianoKeyData
	{
		public NoteData  Note;
		public AudioClip AudioClip;
		public string    KeyText;
	}
}
