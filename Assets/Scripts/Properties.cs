using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{

    public enum Type
    {
        Player,
        FreeCrowd,
        Enemy,
        Obstacle,
    };

    public Type type;

}