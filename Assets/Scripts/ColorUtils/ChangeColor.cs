using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorUtils
{
    public class ChangeColor : MonoBehaviour
    {

        public ColorHelper.AcceptableColor ColorName;
        public GameObject[] ObjectsToRecolor;

        void Start()
        {
            if (ObjectsToRecolor == null || ObjectsToRecolor.Length == 0)
            {
                ObjectsToRecolor = new GameObject[] { gameObject };
            }

            Material material = ColorHelper.GetObjectMaterial(gameObject);
            material = Instantiate(material);
            material.color = ColorHelper.EnumToColor(ColorName);
            foreach (GameObject obj in ObjectsToRecolor)
            {
                ColorHelper.SetObjectMaterial(obj, material);
            }
        }

    }
}