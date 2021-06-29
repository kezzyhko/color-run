using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public static class ColorHelper
    {
        public enum AcceptableColor
        {
            Gray   = 0b000,
            Red    = 0b001,
            Green  = 0b010,
            Blue   = 0b100,
            Yellow = 0b011,
            Pink   = 0b101,
            Cyan   = 0b110,
            Black  = 0b111,
            NoRecolor,
        };

        private static Color[] Colors = new Color[]
        {
            /* 000 */ Color.gray,
            /* 001 */ Color.red,
            /* 010 */ Color.green,
            /* 011 */ Color.red + Color.green,
            /* 100 */ Color.blue,
            /* 101 */ Color.red + Color.blue,
            /* 110 */ Color.green + Color.blue,
            /* 111 */ Color.black,
        };

        public static Color EnumToColor(this AcceptableColor acceptableColor)
        {
            return Colors[(int) acceptableColor];
        }

        public static bool CompareColorsWithoutAlpha(this Color c1, Color c2)
        {
            return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
        }

        public static Material GetObjectMaterial(this GameObject obj)
        {
            return obj.GetRenderers()[0].sharedMaterial;
        }

        public static void SetObjectMaterial(this GameObject obj, Material material)
        {
            foreach (Renderer r in obj.GetRenderers())
            {
                r.sharedMaterial = material;
            }
        }

        public static void SetObjectColor(this GameObject obj, Color color)
        {
            obj.GetObjectMaterial().color = color;
        }

        public static Color GetObjectColor(this GameObject obj)
        {
            return obj.GetObjectMaterial().color;
        }

        public static void SetUIColor(this GameObject obj, Color color)
        {
            obj.GetComponent<Image>().color = color;
        }

        public static Color GetUIColor(this GameObject obj)
        {
            return obj.GetComponent<Image>().color;
        }
    }
}