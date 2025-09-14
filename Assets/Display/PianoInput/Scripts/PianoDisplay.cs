using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Core
{
	public class PianoDisplay : InputInstrumentBase
	{
		public PianoKey  Key;
		public Transform Pivot;
		public float     KeySize = 1.25f;

		List<PianoKey> _keys = new();
		List<(float time, NoteData note)> _currentPlayNotes = new();
		GeneralConfig  _config;
		
		void OnEnable()
		{
			GameEvents.GetCurrentPlayNotes     = () => _currentPlayNotes;
			GameEvents.RequestConsumePlayNotes = (index) => _currentPlayNotes.RemoveAt(index);
		}

		public override void LoadRange(int fromKeyId = 31, int toKeyId = 38)
		{
			while (_keys.Count > 0)
			{
				var key = _keys[0];
				_keys.RemoveAt(0);
				Destroy(key.gameObject);
			}
			
			for (var i = fromKeyId; i <= toKeyId; i++)
			{
				var newKey = Instantiate(Key, Pivot);
				var note   = new NoteData { BaseId = i };
				var audioClip = GameEvents.GetInstrumentNote?.Invoke(i);
				var keyData = new PianoKeyData
				{
					Note    = note,
					KeyText = note.ToText(),
					AudioClip = audioClip?.Clip,
				};

				newKey.gameObject.SetActive(true);
				newKey.SetData(keyData);
				newKey.transform.localPosition =  new Vector3((i - fromKeyId) * KeySize, 0,       0);
				newKey.transform.localScale    =  new Vector3(KeySize,                   KeySize, 1);
				newKey.OnPlayNote              += OnPlayNote;
				_keys.Add(newKey);
			}
		}

		void OnPlayNote(PianoKey key)
		{
			_currentPlayNotes.Add((0, key.Data.Note));
		}

		void Update()
		{
			var config = GameEvents.GetGeneralConfig?.Invoke();
			var hitWindow = config?.HitWindow ?? 0.25f / 2; // Half after

			for (var i = 0; i < _currentPlayNotes.Count; i++)
			{
				var note = _currentPlayNotes[i];
				note.time           += Time.deltaTime;
				_currentPlayNotes[i] =  note;
				if (note.time > hitWindow)
				{
					_currentPlayNotes.RemoveAt(i);
					i--;
				}
			}
		}
	}
}
