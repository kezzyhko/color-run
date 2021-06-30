using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Mechanics.ColorMixing;
using static Utils.PropertiesHelper.ObjectType;

namespace Mechanics.Collisions
{
    public class ObstacleCollision : MonoBehaviour
    {

        ColorMixingManager _colorMixing;

        public void Construct(ColorMixingManager colorMixing)
        {
            _colorMixing = colorMixing;
        }

        void OnTriggerEnter(Collider collider)
        {
            GameObject obstacle = collider.gameObject;
            if (!obstacle.DoesTypeMatch(Obstacle)) return;

            if (_colorMixing.CurrentPlayerColor == obstacle.GetObjectColor())
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