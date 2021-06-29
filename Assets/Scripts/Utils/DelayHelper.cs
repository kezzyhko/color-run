using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class DelayHelper
    {

        public static void DelayedExecute(MonoBehaviour caller, System.Action action, float delay)
        {
            if (delay == 0)
            {
                action();
            }
            else
            {
                caller.StartCoroutine(DelayRoutine(action, delay));
            }
        }

        private static IEnumerator DelayRoutine(System.Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

    }
}