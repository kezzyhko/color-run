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

    private Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.magenta,
        Color.yellow,
        Color.cyan,
        Color.black,
    };

    public AcceptableColor colorName;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = colors[(int)colorName];
        //gameObject.GetComponent<Renderer>().material.color = color;
    }

}
