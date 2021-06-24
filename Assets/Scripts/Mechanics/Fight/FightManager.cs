using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Mechanics.Fight
{
    public class FightManager : MonoBehaviour
    {
        public LinkedList<GameObject> Players = new LinkedList<GameObject>();
        public LinkedList<GameObject> Enemies = new LinkedList<GameObject>();

        public bool IsFightStarted
        {
            get;
            private set;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (IsFightStarted) return;
            IsFightStarted = true;

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