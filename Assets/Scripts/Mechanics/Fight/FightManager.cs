using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Fight
{
    public class FightManager : MonoBehaviour
    {

        public GameObject InitialPlayer;
        public GameObject[] InitialEnemies;

        public LinkedList<GameObject> Players = new LinkedList<GameObject>();
        public LinkedList<GameObject> Enemies;

        private bool _isFightStarted;
        private bool _isFightFinished;

        private LevelManager _levelManager;

        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void Start()
        {
            Players.AddLast(InitialPlayer);
            Enemies = new LinkedList<GameObject>(InitialEnemies);
        }

        void OnTriggerEnter(Collider collider)
        {
            // execute only once
            if (_isFightStarted) return;
            _isFightStarted = true;

            // stop moving forward
            foreach (GameObject player in Players)
            {
                Destroy(player.GetComponent<Movement.MoveForward>());
            }
            Destroy(Camera.main.GetComponent<Movement.MoveForward>());

            // add fighting logic
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
            // no removing after fight finished
            if (_isFightFinished) return;

            // remove character
            team.Remove(character);
            Destroy(character);

            // check if fight is finished
            if (team.Count == 0)
            {
                _isFightFinished = true;
                Destroy(Camera.main.GetComponent<Movement.MoveForward>()); // stop moving camera
                _levelManager.EndLevel(isWin: team == Enemies); // show GUI
            }
        }
    }
}