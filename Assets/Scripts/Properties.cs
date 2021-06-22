using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{

    public enum Type
    {
        Player,
        Free,
        Enemy,
        Obstacle,
    };

    public Type ObjectType;
    public Renderer Renderer;

    private void OnValidate()
    {
        if (Renderer == null)
        {
            Renderer = GetComponent<Renderer>();
        }
    }

}