using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorUtils
{
    public class ChangeColor : MonoBehaviour
    {

        public ColorHelper.AcceptableColor ColorName;
        public bool DestroyWhenFinished = true;

        void Start()
        {
            ColorHelper.SetObjectColor(gameObject, ColorHelper.EnumToColor(ColorName));
            if (DestroyWhenFinished)
            {
                Destroy(this);
            }
        }

    }
}