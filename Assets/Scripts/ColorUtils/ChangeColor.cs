using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorUtils
{
    public class ChangeColor : MonoBehaviour
    {

        public ColorHelper.AcceptableColor ColorName;

        void Start()
        {
            Material material = ColorHelper.GetObjectMaterial(gameObject);
            material = Instantiate(material);
            material.color = ColorHelper.EnumToColor(ColorName);
            ColorHelper.SetObjectMaterial(gameObject, material);
        }

    }
}