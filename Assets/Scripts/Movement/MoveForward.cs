using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class MoveForward : MonoBehaviour
    {

        public float Speed = 2.0f;

        void FixedUpdate()
        {
            transform.position += new Vector3(0, 0, Time.fixedDeltaTime * Speed);
        }
    }
}