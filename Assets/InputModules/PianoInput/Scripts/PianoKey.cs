using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;

namespace PianoInput
{
	public class PianoKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
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
			foreach (var txt in NoteNameTxts) txt.text           = Data.Note.ToText();
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			CurrentState = KeyState.Down;
			RefreshDisplay();
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
		public NoteData Note;
		public string   ClipId;
		public string   KeyCode;
	}
}
