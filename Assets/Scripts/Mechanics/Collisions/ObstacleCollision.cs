using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorUtils;

namespace Mechanics.Collisions
{
    public class ObstacleCollision : MonoBehaviour
    {

        void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            if (!Properties.DoesTypeMatch(obstacle, Properties.Type.Obstacle)) return;

            Color playerColor = ColorHelper.GetObjectColor(gameObject);
            Color obstacleColor = ColorHelper.GetObjectColor(obstacle);
            if (ColorHelper.CompareColorsWithoutAlpha(playerColor, obstacleColor))
            {
                obstacle.AddComponent<Shrink>();
            }
            else
            {
                GetComponent<CharacterManager>().MakeDead();
            }
        }

    }
}