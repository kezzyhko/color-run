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
        new Color(1, 1, 0), // Color.yellow is (1, 0.92, 0.016, 1)
        Color.cyan,
        Color.black,
    };

    public AcceptableColor ColorName;
    public bool DestroyWhenFinished = true;

    void Start()
    {
        GetComponent<Renderer>().material.color = _colors[(int) ColorName];
        if (DestroyWhenFinished)
        {
            Destroy(this);
        }
    }

}