using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class Slide : MonoBehaviour
    {

        public float Speed = 2.0f;
        public float Amplitude = 5.0f;

        private float _initialPosition;

        private void OnValidate()
        {
            Amplitude = Mathf.Max(Amplitude, float.Epsilon);
        }

        private void Start()
        {
            _initialPosition = transform.position.x;
        }

        void Update()
        {
            float posDiff = _initialPosition - transform.position.x;
            if (Mathf.Abs(posDiff) > Amplitude / 2)
            {
                Speed = Mathf.Sign(posDiff) * Mathf.Abs(Speed);
            }
            transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
        }
    }
}