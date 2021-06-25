using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class Rotate : MonoBehaviour
    {

        public Vector3 Speed = new Vector3(0, 90, 0);

        void Update()
        {
            transform.Rotate(Time.deltaTime * Speed);
        }
    }
}