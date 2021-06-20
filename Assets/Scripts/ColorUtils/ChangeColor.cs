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

            foreach (GameObject obj in ObjectsToRecolor)
            {
                ColorHelper.SetObjectColor(obj, ColorHelper.EnumToColor(ColorName));
            }
        }

    }
}