using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorMixing : MonoBehaviour
{

    public GameObject CurrentColorIndicator;
    public float LineThickness = 8.0f;
    public Material PlayerMaterial;

    private bool _isSelectingInProcess = false;
    private Vector2 _startLinePosition;
    private LinkedList<(Vector2, Vector2)> _lines = new LinkedList<(Vector2, Vector2)>();
    private LinkedList<Color> _colors = new LinkedList<Color>();
    private Color _mixedColor = Color.black;

    void Start()
    {
        PlayerMaterial = Instantiate(PlayerMaterial);
        PlayerMaterial.name = "Player Material";
        PlayerMaterial.color = _mixedColor;
        CurrentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = _mixedColor;
    }

    public static bool CompareWithoutAlpha(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
    }

    void UpdateMixedColor(GameObject sender)
    {
        Color newColor = sender.GetComponent<UnityEngine.UI.Image>().color;
        _colors.AddLast(newColor);
        _mixedColor += newColor;
        if (CompareWithoutAlpha(_mixedColor, Color.white)) _mixedColor = Color.black;
        CurrentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = _mixedColor;
    }

    void ChangePlayersColor(Color newColor)
    {
        PlayerMaterial.color = newColor;
    }

    public void PointerClick(GameObject sender)
    {
        if (_isSelectingInProcess) return;
        Color newColor = sender.GetComponent<UnityEngine.UI.Image>().color;
        CurrentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = newColor;
        ChangePlayersColor(newColor);
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
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
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
        ChangePlayersColor(_mixedColor);
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