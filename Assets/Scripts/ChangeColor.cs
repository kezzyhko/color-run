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

    public static Color[] Colors = new Color[]
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
        GetComponent<Renderer>().material.color = Colors[(int) ColorName];
        if (DestroyWhenFinished)
        {
            Destroy(this);
        }
    }

}