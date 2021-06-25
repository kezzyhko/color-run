using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorUtils;
using static Properties.Type;

namespace Mechanics.Collisions
{
    public class ObstacleCollision : MonoBehaviour
    {

        void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            if (!Properties.DoesTypeMatch(obstacle, Obstacle)) return;

            Color playerColor = ColorHelper.GetObjectColor(gameObject);
            Color obstacleColor = ColorHelper.GetObjectColor(obstacle);
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