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
        Properties props = obj.GetComponent<Properties>();
        return props!= null && props.ObjectType == type;
    }

}