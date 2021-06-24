using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Collisions
{
    public class GatherFreeCrowd : MonoBehaviour
    {

        public LinkedList<GameObject> AdjacentFreeCrowd = new LinkedList<GameObject>();

        void OnTriggerEnter(Collider collider)
        {
            GameObject free = collider.gameObject;
            if (!Properties.DoesTypeMatch(free, Properties.Type.Free)) return;
            if (Properties.DoesTypeMatch(gameObject, Properties.Type.Free))
            {
                AdjacentFreeCrowd.AddLast(free);
                return;
            }

            free.GetComponent<CharacterManager>().MakePlayer();
        }

    }
}