using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static Utils.PropertiesHelper.ObjectType;

namespace Mechanics.Collisions
{
    public class GatherFreeCrowd : MonoBehaviour
    {

        public LinkedList<GameObject> AdjacentFreeCrowd = new LinkedList<GameObject>();

        void OnTriggerEnter(Collider collider)
        {
            GameObject free = collider.gameObject;
            if (!free.DoesTypeMatch(Free)) return;
            if (gameObject.DoesTypeMatch(Free))
            {
                AdjacentFreeCrowd.AddLast(free);
                return;
            }

            free.GetComponent<CharacterManager>().MakePlayer();
        }

    }
}