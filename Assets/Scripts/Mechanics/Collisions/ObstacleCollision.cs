using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorUtils;
using Mechanics.Fight;

namespace Mechanics.Collisions
{
    public class ObstacleCollision : MonoBehaviour
    {
        
        private LevelInfo _levelInfo;

        public void Construct(LevelInfo levelInfo)
        {
            _levelInfo = levelInfo;
        }

        void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            if (!Properties.DoesTypeMatch(obstacle, Properties.Type.Obstacle)) return;

            Color playerColor = ColorHelper.GetObjectColor(gameObject);
            Color obstacleColor = ColorHelper.GetObjectColor(obstacle);
            if (ColorHelper.CompareColorsWithoutAlpha(playerColor, obstacleColor))
            {
                Destroy(obstacle);
            }
            else
            {
                FightManager fight = _levelInfo.FightTrigger.GetComponent<FightManager>();
                fight.RemoveCharacter(gameObject, fight.Players);
            }
        }

    }
}