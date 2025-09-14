using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
	public class MenuUI : MonoBehaviour
	{
		public Button        PrefabButton;
		public RectTransform VerticleRoot;
		
		public void SetData(List<LessonData> lessons)
		{
			// Clear existing buttons
			foreach (Transform child in VerticleRoot)
			{
				Destroy(child.gameObject);
			}

			// Create new buttons
			for (var i = 0; i < lessons.Count; i++)
			{
				var lesson = lessons[i];
				var button = Instantiate(PrefabButton, VerticleRoot);
				button.gameObject.SetActive(true);
				button.onClick.AddListener(() => OnUIClick(lesson));
				var ugui                    = button.GetComponentInChildren<TextMeshProUGUI>();
				if (ugui != null) ugui.text = lesson.LessonName;
			}
		}

		public void OnUIClick(LessonData lesson)
		{
			GameEvents.UIRequestStartLesson?.Invoke(lesson);
		}
	}
}
