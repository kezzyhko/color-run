using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class PropertiesHelper
    {

        public enum ObjectType
        {
            Other,
            Player,
            Free,
            Enemy,
            Obstacle,
        };

        public static bool DoesTypeMatch(this GameObject obj, ObjectType type)
        {
            if (!obj.TryGetComponent(out Properties props)) return false;
            return props.Type == type;
        }

        public static Renderer[] GetRenderers(this GameObject obj)
        {
            if (!obj.TryGetComponent(out Properties props)) return null;
            return props.Renderers;
        }
    }
}