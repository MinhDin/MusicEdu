using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace Core
{
	/// <summary>
	/// Main controller class that manages game state transitions between menu and lessons,
	/// coordinates initialization of core game systems, and handles lesson loading/unloading.
	/// Responsible for downloading and managing lesson configurations, and orchestrating
	/// the communication between UI, gameplay systems and lesson management.
	/// </summary>
	public class MainGame : MonoBehaviour
	{
		public ThemeManager           ThemeManager;
		public SongManager            SongManager;
		public SoundInstrumentManager SoundInstrumentManager;
		public DisplayManager         DisplayManager;
		public GeneralConfig          GeneralConfig;
		public GameCamera             GameCamera;
		public TextAsset LessonData;
		
		// UI
		public PlayingUI PlayingUI;
		public MenuUI    MenuUI;

		List<LessonData> _allLesson;
		Lesson           _currentLesson;
		string           _downloadedConfig;

		public enum State
		{
			Menu,
			Lesson,
		}
		
		State _state = State.Menu;
		
		IEnumerator Start()
		{
			Input.multiTouchEnabled     = false;
			GameEvents.GetGeneralConfig = () => GeneralConfig;
			GameEvents.RequestEndLesson = EndLesson;
			GameEvents.UIRequestStartLesson = StartLesson;
			
			GameCamera.Init();
			SoundInstrumentManager.Init();
			yield return null;
			ThemeManager.Init();
			yield return null;
			SongManager.Init();
			DisplayManager.Init();

			yield return null;
			_currentLesson = new Lesson();
			PlayingUI.Lesson = _currentLesson;
			
			yield return StartCoroutine(DownloadConfigCoroutine());

			//_currentLesson.LoadLesson(config);
			var json = string.IsNullOrEmpty(_downloadedConfig) ? LessonData.text : _downloadedConfig;
			_allLesson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LessonData>>(json);
			
			_state = State.Menu;
			MenuUI.gameObject.SetActive(true);
			PlayingUI.gameObject.SetActive(false);
			MenuUI.SetData(_allLesson);
		}

		private void StartLesson(LessonData config)
		{
			_currentLesson.LoadLesson(config);
			_state = State.Lesson;
			MenuUI.gameObject.SetActive(false);
			PlayingUI.gameObject.SetActive(true);
		}
		
		private IEnumerator DownloadConfigCoroutine()
		{
			var url = "https://drive.google.com/uc?export=download&id=1CmXNVmAo34EWBbYfLXbfJhS_FjncBhuP";
			using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
			{
				yield return webRequest.SendWebRequest();

				if (webRequest.result == UnityWebRequest.Result.Success)
				{
					_downloadedConfig = webRequest.downloadHandler.text;
				}
				else
				{
					_downloadedConfig = string.Empty;
					Debug.LogError($"Config download failed: {webRequest.error}");
				}
			}
		}

		void Update()
		{
			if (_currentLesson == null) return;
			
			if(_state == State.Lesson) _currentLesson.Update(Time.deltaTime);
		}

		public void EndLesson(LessonResult result)
		{
			Debug.Log($"Lesson Result: {JsonUtility.ToJson(result)}");
			_state = State.Menu;
			_currentLesson?.LessonEnd();
			MenuUI.gameObject.SetActive(true);
			PlayingUI.gameObject.SetActive(false);
		}
	}
}
