using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Properties.Type;

namespace Mechanics.Collisions
{
    public class GatherFreeCrowd : MonoBehaviour
    {

        public LinkedList<GameObject> AdjacentFreeCrowd = new LinkedList<GameObject>();

        void OnTriggerEnter(Collider collider)
        {
            GameObject free = collider.gameObject;
            if (!Properties.DoesTypeMatch(free, Free)) return;
            if (Properties.DoesTypeMatch(gameObject, Free))
            {
                AdjacentFreeCrowd.AddLast(free);
                return;
            }

            free.GetComponent<CharacterManager>().MakePlayer();
        }

    }
}