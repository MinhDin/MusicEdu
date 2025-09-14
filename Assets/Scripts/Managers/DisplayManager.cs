using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System;

namespace Core
{
	/// <summary> Handle display part of the game </summary>
	public class DisplayManager : MonoBehaviour
	{
		public PianoDisplay PianoDisplay;
		public SheetMusicDisplay SheetMusicDisplay;
		
		Dictionary<Type, Dictionary<string, MonoBehaviour>> _displayObjs = new();

		public void Init()
		{
			_displayObjs.Add(typeof(InputInstrumentBase), new Dictionary<string, MonoBehaviour>());
			_displayObjs.Add(typeof(SheetMusicDisplay),   new Dictionary<string, MonoBehaviour>());
			
			_displayObjs[typeof(InputInstrumentBase)].Add("Piano", PianoDisplay);
			_displayObjs[typeof(SheetMusicDisplay)].Add("BasicSheet", SheetMusicDisplay);
			
			GameEvents.GetDisplayObj = (type, instrumentId) => _displayObjs.GetValueOrDefault(type)?.GetValueOrDefault(instrumentId); 
		}
	}
}
