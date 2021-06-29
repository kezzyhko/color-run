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

            Color playerColor = gameObject.GetObjectColor();
            Color obstacleColor = obstacle.GetObjectColor();
            if (ColorHelper.CompareColorsWithoutAlpha(playerColor, obstacleColor))
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