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

            Camera.main.GetComponent<MoveForward>().enabled = false;

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