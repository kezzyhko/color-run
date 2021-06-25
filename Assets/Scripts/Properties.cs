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

    public static bool DoesTypeMatch(GameObject obj, Type type)
    {
        if (!obj.TryGetComponent(out Properties props)) return false;
        return props.ObjectType == type;
    }

}