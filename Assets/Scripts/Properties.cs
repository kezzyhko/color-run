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
    public Renderer[] Renderers;

    public static bool DoesTypeMatch(GameObject obj, Type type)
    {
        if (!obj.TryGetComponent(out Properties props)) return false;
        return props.ObjectType == type;
    }

    public static Renderer[] GetRenderers(GameObject obj)
    {

        if (!obj.TryGetComponent(out Properties props)) return null;
        return props.Renderers;
    }

    private void OnValidate()
    {
        if (Renderers == null || Renderers.Length == 0)
        {
            Renderers = GetComponentsInChildren<Renderer>();
        }
    }

}