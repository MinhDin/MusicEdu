using System;
using System.Collections;
using UnityEngine;

namespace Core
{
	public class MainGame : MonoBehaviour
	{
		public ThemeManager ThemeManager;
		
		IEnumerator Start()
		{
			ThemeManager.Init();
			yield return null;
		}
	}
}
