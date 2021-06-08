using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    private Color targetColor;
    private GameObject player;
    private GameObject currentColorIndicator;

    void Start()
    {
        targetColor = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        player = GameObject.Find("Player");
        currentColorIndicator = GameObject.Find("CurrentColor");
    }

    public void UpdateColor()
    {
        player.GetComponent<Renderer>().material.color = targetColor;
        currentColorIndicator.GetComponent<UnityEngine.UI.Image>().color = targetColor;
    }
}
