using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorUtils
{
    public class ChangeMillColor : MonoBehaviour
    {

        public ColorHelper.AcceptableColor ColorName;

        [SerializeField, HideInInspector]
        private GameObject[] _blades;

        void Start()
        {
            foreach (GameObject blade in _blades)
            {
                ChangeColor changeColor = blade.AddComponent<ChangeColor>();
                changeColor.ColorName = ColorName;
            }
        }

    }
}