using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Mechanics.ColorMixing
{
    public class ColorMixingManager : MonoBehaviour
    {

        public GameObject CurrentColorIndicator;
        public float LineThickness = 8.0f;
        public Material PlayerMaterial;
        public ColorHelper.AcceptableColor InitialColor = ColorHelper.AcceptableColor.Black;

        private bool _isSelectingInProcess = false;
        private Vector2 _startLinePosition;
        private LinkedList<(Vector2, Vector2)> _lines = new LinkedList<(Vector2, Vector2)>();
        private LinkedList<Color> _colors = new LinkedList<Color>();
        private Color _mixedColor;

        void Start()
        {
            PlayerMaterial = Instantiate(PlayerMaterial);
            PlayerMaterial.name = "Player Material";
        }

        public void AbortSelection()
        {
            _isSelectingInProcess = false;
            _lines.Clear();
            _colors.Clear();
        }

        public void ResetColor()
        {
            Color initialColor = InitialColor.EnumToColor();
            _mixedColor = initialColor;
            PlayerMaterial.color = initialColor;
            CurrentColorIndicator.SetUIColor(initialColor);
        }

        void UpdateMixedColor(GameObject sender)
        {
            Color newColor = sender.GetUIColor();
            _colors.AddLast(newColor);
            _mixedColor += newColor;
            if (ColorHelper.CompareColorsWithoutAlpha(_mixedColor, Color.white)) _mixedColor = Color.black;
            CurrentColorIndicator.SetUIColor(_mixedColor);
        }

        public void PointerClick(GameObject sender)
        {
            if (_isSelectingInProcess) return;
            Color newColor = sender.GetUIColor();
            newColor.a = 1;
            CurrentColorIndicator.SetUIColor(newColor);
            PlayerMaterial.color = newColor;
        }

        public void BeginDrag(GameObject sender)
        {
            _startLinePosition = sender.transform.position;
            _isSelectingInProcess = true;
            _mixedColor = Color.black;
            UpdateMixedColor(sender);
        }

        public void PointerEnter(GameObject sender)
        {
            if (!_isSelectingInProcess) return;
            Color color = sender.GetUIColor();
            if (_colors.Contains(color)) return;

            _lines.AddLast((_startLinePosition, sender.transform.position));
            _startLinePosition = sender.transform.position;
            UpdateMixedColor(sender);
        }

        public void EndDrag(GameObject sender)
        {
            _isSelectingInProcess = false;
            _lines.Clear();
            _colors.Clear();
            PlayerMaterial.color = _mixedColor;
        }

        void OnGUI()
        {
            foreach ((Vector2 start, Vector2 end) line in _lines)
            {
                DrawLine(line.start, line.end);
            }
            if (_isSelectingInProcess)
            {
                DrawLine(_startLinePosition, Input.mousePosition);
            }
        }

        void DrawLine(Vector2 pointA, Vector2 pointB)
        {
            // fix flipped y coordinate
            pointA.y = Screen.height - pointA.y;
            pointB.y = Screen.height - pointB.y;

            // calculate angle and rotate
            Matrix4x4 matrixBackup = GUI.matrix;
            float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180f / Mathf.PI;
            GUIUtility.RotateAroundPivot(angle, pointA);

            // prepare texture
            Texture2D lineTex = new Texture2D(1, 1);
            GUI.color = Color.black;

            // calculate
            float startX = pointA.x - LineThickness / 2;
            float startY = pointA.y - LineThickness / 2;
            float length = (pointB - pointA).magnitude + LineThickness;

            // draw
            GUI.DrawTexture(new Rect(startX, startY, length, LineThickness), lineTex);

            // restore rotation
            GUI.matrix = matrixBackup;
        }

    }
}