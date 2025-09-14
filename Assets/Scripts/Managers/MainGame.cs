using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace Core
{
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
		
		Lesson _currentLesson;
		string _downloadedConfig;

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

			//var config = JsonUtility.FromJson<LessonData>(_downloadedConfig);
			//_currentLesson.LoadLesson(config);
			_state = State.Menu;
			
			StartCoroutine(StartLesson());
		}

		private IEnumerator StartLesson()
		{
			yield return null;
			_currentLesson.LoadLesson(LessonData.text);
			_state = State.Lesson;
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
		}
	}
}
