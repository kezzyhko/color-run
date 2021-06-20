using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorUtils;

namespace Mechanics
{
    public class ObstacleCollision : MonoBehaviour
    {

        [SerializeField]
        private Fight.FightManager _fight;

        void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            Properties props = obstacle.GetComponent<Properties>();
            if (props == null || props.ObjectType != Properties.Type.Obstacle) return;

            Color playerColor = ColorHelper.GetObjectColor(gameObject);
            Color obstacleColor = ColorHelper.GetObjectColor(obstacle);
            if (ColorHelper.CompareColorsWithoutAlpha(playerColor, obstacleColor))
            {
                Destroy(obstacle);
            }
            else
            {
                _fight.RemoveCharacter(gameObject, _fight.Players);
            }
        }

    }
}