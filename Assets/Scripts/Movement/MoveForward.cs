using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class MoveForward : MonoBehaviour
    {

        public float Speed = 2.0f;

        void Update()
        {
            transform.position += new Vector3(0, 0, Time.deltaTime * Speed);
        }

        private void OnEnable()
        {
            RunningAnimation(true);
        }

        private void OnDisable()
        {
            RunningAnimation(false);
        }

        private void RunningAnimation(bool isRunning)
        {
            Properties props = GetComponent<Properties>();
            if (props == null || props.Animator == null || props.Animator.runtimeAnimatorController == null) return;
            props.Animator.SetBool("running", isRunning);
        }
    }
}