using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Fight
{
    public class FightManager : MonoBehaviour
    {

        public LevelInfo LevelInfo;

        public LinkedList<GameObject> Players = new LinkedList<GameObject>();
        public LinkedList<GameObject> Enemies = new LinkedList<GameObject>();

        private bool _isFightStarted;
        private bool _isFightFinished;

        private LevelManager _levelManager;

        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void Start()
        {
            Players.AddLast(LevelInfo.Player);
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

        public void RemoveCharacter(GameObject character, LinkedList<GameObject> team)
        {
            if (_isFightFinished) return;

            team.Remove(character);
            Destroy(character);

            // check if fight is finished
            if (team.Count == 0)
            {
                _isFightFinished = true;
                Destroy(Camera.main.GetComponent<Movement.MoveForward>());
                _levelManager.EndLevel(isWin: team == Enemies);
            }
        }
    }
}