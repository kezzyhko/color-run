using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColorUtils
{
    public static class ColorHelper
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

        private static Color[] Colors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.red + Color.blue,
            Color.red + Color.green,
            Color.green + Color.blue,
            Color.black,
        };

        public static Color EnumToColor(AcceptableColor acceptableColor)
        {
            return Colors[(int) acceptableColor];
        }

        public static bool CompareColorsWithoutAlpha(Color c1, Color c2)
        {
            return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
        }

        public static Material GetObjectMaterial(GameObject obj)
        {
            return obj.GetComponentInChildren<Renderer>().sharedMaterial;
        }

        public static void SetObjectMaterial(GameObject obj, Material material)
        {
            foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
            {
                r.sharedMaterial = material;
            }
        }

        public static void SetObjectColor(GameObject obj, Color color)
        {
            GetObjectMaterial(obj).color = color;
        }

        public static Color GetObjectColor(GameObject obj)
        {
            return GetObjectMaterial(obj).color;
        }

        public static void SetUIColor(GameObject obj, Color color)
        {
            obj.GetComponent<Image>().color = color;
        }

        public static Color GetUIColor(GameObject obj)
        {
            return obj.GetComponent<Image>().color;
        }
    }
}