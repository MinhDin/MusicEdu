using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
	public class PlayingUI : MonoBehaviour
	{
		public Lesson Lesson { get; set; }
		
		public TextMeshProUGUI BPMText;

		public void Update()
		{
			if (Lesson == null) return;
			
			BPMText.text = $"BPM : {Lesson.CurrentBPM}";
		}
	}
}
