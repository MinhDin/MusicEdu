using UnityEngine;

namespace Core
{
	public class GameCamera : MonoBehaviour
	{
		public Camera  Camera;
		public Vector2 MinPivot;
		public Vector2 MaxPivot;

		public void Init()
		{
			var width = MaxPivot.x - MinPivot.x;
			var height = MaxPivot.y - MinPivot.y;
			var screenRatio = (float)Screen.width / Screen.height;
			var targetRatio = width / height;

			if (screenRatio >= targetRatio)
			{
				Camera.orthographicSize = height / 2;
			}
			else
			{
				var differenceInSize = targetRatio / screenRatio;
				Camera.orthographicSize = height / 2 * differenceInSize;
			}
			
			transform.position = new Vector3((MinPivot.x + MaxPivot.x) / 2, (MinPivot.y + MaxPivot.y) / 2, -30);
		}
	}
}
