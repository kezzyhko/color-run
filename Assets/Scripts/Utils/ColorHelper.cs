using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mechanics.ColorMixing;

namespace Utils
{
    public static class ColorHelper
    {
        public enum AcceptableColor
        {
            None = 0,
            Red    = 0b001,
            Green  = 0b010,
            Blue   = 0b100,
            Yellow = 0b011,
            Pink   = 0b101,
            Cyan   = 0b110,
            Black  = 0b111,
            Gray,
        };

        private static Color[] Colors = new Color[]
        {
            /* 001 */ Color.red,
            /* 010 */ Color.green,
            /* 011 */ Color.red + Color.green,
            /* 100 */ Color.blue,
            /* 101 */ Color.red + Color.blue,
            /* 110 */ Color.green + Color.blue,
            /* 111 */ Color.black,
            Color.gray,
        };

        public static Color EnumToColor(this AcceptableColor acceptableColor)
        {
            return Colors[(int) acceptableColor - 1];
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

        public static void SetObjectColor(this GameObject obj, AcceptableColor color)
        {
            obj.GetComponent<Properties>().ColorName = color;
            obj.GetObjectMaterial().color = color.EnumToColor();
        }

        public static AcceptableColor GetObjectColor(this GameObject obj)
        {
            return obj.GetComponent<Properties>().ColorName;
        }

        public static void SetUIColor(this GameObject obj, AcceptableColor color)
        {
            obj.GetComponent<Image>().color = color.EnumToColor();
        }

        public static AcceptableColor GetUIColor(this GameObject obj)
        {
            return obj.GetComponent<ColorMixingButton>().ColorName;
        }
    }
}