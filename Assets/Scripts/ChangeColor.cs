using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    public enum AcceptableColor
    {
        Red,
        Green,
        Blue,
        Pink,
        Yellow,
        Cyan,
        Black,
    };

    private Color[] _colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.magenta,
        Color.yellow,
        Color.cyan,
        Color.black,
    };

    public AcceptableColor ColorName;

    void Start()
    {
        GetComponent<Renderer>().material.color = _colors[(int) ColorName];
    }

}