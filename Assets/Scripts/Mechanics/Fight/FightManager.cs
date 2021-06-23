using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Mechanics.Fight
{
    public class FightManager : MonoBehaviour
    {

        public LevelInfo LevelInfo;

        public LinkedList<GameObject> Players = new LinkedList<GameObject>();
        public LinkedList<GameObject> Enemies = new LinkedList<GameObject>();

        private bool _isFightStarted;

        private void Start()
        {
            foreach (Transform enemyTransform in LevelInfo.EnemyCrowd.transform)
            {
                Enemies.AddLast(enemyTransform.gameObject);
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (_isFightStarted) return;
            _isFightStarted = true;

            foreach (GameObject player in Players)
            {
                Destroy(player.GetComponent<Movement.MoveForward>());
            }
            Destroy(Camera.main.GetComponent<Movement.MoveForward>());

            AddScript(Players, shouldDestroy: true);
            AddScript(Enemies, shouldDestroy: false);
        }

        private void AddScript(LinkedList<GameObject> team, bool shouldDestroy)
        {
            foreach (GameObject character in team)
            {
                FightBehaviour moveTowardsTarget = character.AddComponent<FightBehaviour>();
                moveTowardsTarget.Fight = this;
                moveTowardsTarget.ShouldDestroy = shouldDestroy;
            }
        }

    }
}