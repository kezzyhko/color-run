using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorMixing : MonoBehaviour
{

    public GameObject currentColorIndicator;
    public GameObject initialPlayer;
    public LinkedList<GameObject> players = new LinkedList<GameObject>();

    public float lineThickness = 8.0f;

    private bool isSelectingInProcess = false;
    private Vector2 startLinePosition;
    private LinkedList<(Vector2, Vector2)> lines = new LinkedList<(Vector2, Vector2)>();
    private LinkedList<Color> colors = new LinkedList<Color>();
    private Color mixedColor;

    void Start()
    {
        players.AddLast(initialPlayer);
        mixedColor = initialPlayer.GetComponent<Renderer>().material.color;
        currentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = mixedColor;
    }

    public static bool CompareWithoutAlpha(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
    }

    void UpdateMixedColor(GameObject sender)
    {
        Color newColor = sender.GetComponent<UnityEngine.UI.Image>().color;
        colors.AddLast(newColor);
        mixedColor += newColor;
        if (CompareWithoutAlpha(mixedColor, Color.white)) mixedColor = Color.black;
        currentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = mixedColor;
    }

    void ChangePlayersColor(Color newColor)
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Renderer>().material.color = newColor;
        }
    }

    public void PointerClick(GameObject sender)
    {
        Color newColor = sender.GetComponent<UnityEngine.UI.Image>().color;
        currentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = newColor;
        ChangePlayersColor(newColor);
    }

    public void BeginDrag(GameObject sender)
    {
        startLinePosition = sender.transform.position;
        isSelectingInProcess = true;
        mixedColor = Color.black;
        UpdateMixedColor(sender);
    }

    public void PointerEnter(GameObject sender)
    {
        if (!isSelectingInProcess) return;
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
        if (colors.Contains(color)) return;

        lines.AddLast((startLinePosition, sender.transform.position));
        startLinePosition = sender.transform.position;
        UpdateMixedColor(sender);
    }

    public void EndDrag(GameObject sender)
    {
        isSelectingInProcess = false;
        lines.Clear();
        colors.Clear();
        ChangePlayersColor(mixedColor);
    }

    void OnGUI()
    {
        foreach ((Vector2 start, Vector2 end) line in lines)
        {
            DrawLine(line.start, line.end);
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