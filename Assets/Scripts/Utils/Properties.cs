using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils.ColorHelper.AcceptableColor;

namespace Utils
{
    public class Properties : MonoBehaviour
    {

        public PropertiesHelper.ObjectType Type;
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
                Material material = gameObject.GetObjectMaterial();
                material = Instantiate(material);
                material.color = ColorName.EnumToColor();
                gameObject.SetObjectMaterial(material);
            }
        }

    }
}