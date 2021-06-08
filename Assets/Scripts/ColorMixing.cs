using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorMixing : MonoBehaviour
{

    public float lineThickness = 8.0f;

    private bool isSelectingInProcess = false;
    private Vector2 startLinePosition;
    private LinkedList<Tuple<Vector2, Vector2>> lines = new LinkedList<Tuple<Vector2, Vector2>>();
    private LinkedList<Color> colors = new LinkedList<Color>();
    private Color mixedColor = Color.black;

    void Start()
    {
        GameObject.Find("Player").GetComponent<Renderer>().material.color = Color.black;
    }

    void UpdateColor(GameObject sender)
    {
        Color newColor = sender.GetComponent<UnityEngine.UI.Image>().color;
        colors.AddLast(newColor);
        mixedColor += newColor;
        if (mixedColor.r == 1 && mixedColor.g == 1 && mixedColor.b == 1) mixedColor = Color.black;
        GameObject.Find("CurrentColor").GetComponent<UnityEngine.UI.Image>().color = mixedColor;
    }

    public void PointerClick(GameObject sender)
    {
        Color newColor = sender.GetComponent<UnityEngine.UI.Image>().color;
        GameObject.Find("CurrentColor").GetComponent<UnityEngine.UI.Image>().color = newColor;
        GameObject.Find("Player").GetComponent<Renderer>().material.color = newColor;
    }

    public void BeginDrag(GameObject sender)
    {
        startLinePosition = sender.transform.position;
        isSelectingInProcess = true;
        mixedColor = Color.black;
        UpdateColor(sender);
    }

    public void PointerEnter(GameObject sender)
    {
        if (!isSelectingInProcess) return;
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
        if (colors.Contains(color)) return;

        lines.AddLast(new Tuple<Vector2, Vector2>(startLinePosition, sender.transform.position));
        startLinePosition = sender.transform.position;
        UpdateColor(sender);
    }

    public void EndDrag(GameObject sender)
    {
        isSelectingInProcess = false;
        lines.Clear();
        colors.Clear();
        GameObject.Find("Player").GetComponent<Renderer>().material.color = mixedColor;
    }

    void OnGUI()
    {
        foreach (Tuple<Vector2, Vector2> line in lines)
        {
            DrawLine(line.Item1, line.Item2);
        }
        if (isSelectingInProcess)
        {
            DrawLine(startLinePosition, Input.mousePosition);
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
        float startX = pointA.x - lineThickness / 2;
        float startY = pointA.y - lineThickness / 2;
        float length = (pointB - pointA).magnitude + lineThickness;

        // draw
        GUI.DrawTexture(new Rect(startX, startY, length, lineThickness), lineTex);

        // restore rotation
        GUI.matrix = matrixBackup;
    }

}