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
        public ParticleSystem ParticleSystem;

        private void OnValidate()
        {
            if (Renderers == null || Renderers.Length == 0)
            {
                Renderers = GetComponentsInChildren<Renderer>();
            }
            if (ParticleSystem == null)
            {
                ParticleSystem = GetComponentInChildren<ParticleSystem>();
            }
        }

        void Start()
        {
            if (ColorName != None)
            {
                Material material = gameObject.GetObjectMaterial();
                material = Instantiate(material);
                material.color = ColorName.EnumToColor();
                gameObject.SetObjectMaterial(material);
            }
            else
            {
                ColorName = transform.parent.gameObject.GetColor();
            }
        }

    }
}