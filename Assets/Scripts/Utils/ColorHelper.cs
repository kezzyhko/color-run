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
            LightBlue,
            VeryLightBlue,
        };

        private static Color[] Colors = new Color[]
        {
            0xF87B84.RgbToColor(), // 001
            0xBCE086.RgbToColor(), // 010
            0xFFE724.RgbToColor(), // 011
            0x0083C2.RgbToColor(), // 100
            0xFCB2D7.RgbToColor(), // 101
            0x5DE0F5.RgbToColor(), // 110
            0x606060.RgbToColor(), // 111
            0xD4D4D4.RgbToColor(), // gray
            0xBBE0F3.RgbToColor(), // light blue
            0xE8F7FF.RgbToColor(), // very light blue
        };

        public static Color EnumToColor(this AcceptableColor acceptableColor)
        {
            return Colors[(int) acceptableColor - 1];
        }

        public static Color RgbToColor(this int rgb)
        {
            int r = (rgb & 0xff0000) >> 16;
            int g = (rgb & 0x00ff00) >> 8;
            int b = (rgb & 0x0000ff);
            return new Color(r/255f, g/255f, b/255f);
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