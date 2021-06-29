using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.ColorHelper.AcceptableColor;

namespace Utils
{
    public class Properties : MonoBehaviour
    {

        public enum Type
        {
            Other,
            Player,
            Free,
            Enemy,
            Obstacle,
        };

        public Type ObjectType;
        public ColorHelper.AcceptableColor ColorName;
        public Renderer[] Renderers;

        private void OnValidate()
        {
            if (Renderers == null || Renderers.Length == 0)
            {
                Renderers = GetComponentsInChildren<Renderer>();
            }
        }

        void Start()
        {
            if (ColorName != NoRecolor)
            {
                Material material = ColorHelper.GetObjectMaterial(gameObject);
                material = Instantiate(material);
                material.color = ColorHelper.EnumToColor(ColorName);
                ColorHelper.SetObjectMaterial(gameObject, material);
            }
        }

    }
}