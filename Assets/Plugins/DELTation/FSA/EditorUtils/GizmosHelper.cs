using UnityEngine;

namespace DELTation.FSA.EditorUtils
{
	public static class GizmosHelper
	{
		public static void DrawArrow(Vector3 from, Vector3 to, float segmentsLength = 1f, float angle = 30f,
			float arrowPointRatio = 1f)
		{
			Gizmos.DrawLine(from, to);

			var direction = (to - from).normalized;
			var arrowPoint = Vector3.Lerp(from, to, arrowPointRatio);
			var segment1 = Quaternion.Euler(0f, 0f, angle) * -direction * segmentsLength;
			var segment2 = Quaternion.Euler(0f, 0f, -angle) * -direction * segmentsLength;
			Gizmos.DrawRay(arrowPoint, segment1);
			Gizmos.DrawRay(arrowPoint, segment2);
		}

		public static void DrawText(Vector3 position, string text, Color? color = null, int fontSize = 0,
			float yOffset = 0)
		{
#if UNITY_EDITOR
			var guiSkin = GUI.skin;

			var prevSkin = GUI.skin;
			if (guiSkin == null)
				Debug.LogWarning("editor warning: guiSkin parameter is null");
			else
				GUI.skin = guiSkin;

			var textContent = new GUIContent(text);

			var style = guiSkin != null ? new GUIStyle(guiSkin.GetStyle("Label")) : new GUIStyle();
			if (color != null)
				style.normal.textColor = (Color) color;
			if (fontSize > 0)
				style.fontSize = fontSize;

			var textSize = style.CalcSize(textContent);
			var screenPoint = Camera.current.WorldToScreenPoint(position);

			if (screenPoint.z > 0)
			{
				var worldPosition = Camera.current.ScreenToWorldPoint(new Vector3(screenPoint.x - textSize.x * 0.5f,
					screenPoint.y + textSize.y * 0.5f + yOffset, screenPoint.z));
				UnityEditor.Handles.Label(worldPosition, textContent, style);
			}

			GUI.skin = prevSkin;
#endif
		}
	}
}