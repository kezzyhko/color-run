using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class DelayHelper
    {

        public static void DelayedExecute(this MonoBehaviour caller, System.Action action, float delay)
        {
            if (delay == 0)
            {
                action();
            }
            else
            {
                caller.StartCoroutine(Routine(action, delay));
            }
        }

        private static IEnumerator Routine(System.Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

    }
}