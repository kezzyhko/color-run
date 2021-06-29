using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class PropertiesHelper
    {
        public static bool DoesTypeMatch(this GameObject obj, Properties.Type type)
        {
            if (!obj.TryGetComponent(out Properties props)) return false;
            return props.ObjectType == type;
        }

        public static Renderer[] GetRenderers(this GameObject obj)
        {
            if (!obj.TryGetComponent(out Properties props)) return null;
            return props.Renderers;
        }
    }
}