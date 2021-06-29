using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using static Utils.PropertiesHelper.ObjectType;

namespace Mechanics.Collisions
{
    public class ObstacleCollision : MonoBehaviour
    {

        void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            if (!obstacle.DoesTypeMatch(Obstacle)) return;

            if (gameObject.GetObjectColor() == obstacle.GetObjectColor())
            {
                bool shrinkAlreadyAdded = obstacle.TryGetComponent<Shrink>(out _);
                if (!shrinkAlreadyAdded) obstacle.AddComponent<Shrink>();
            }
            else
            {
                GetComponent<CharacterManager>().MakeDead();
            }
        }

    }
}