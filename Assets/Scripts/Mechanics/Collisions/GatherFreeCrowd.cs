using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Collisions
{
    public class GatherFreeCrowd : MonoBehaviour
    {

        public GameObject PlayerPrefab;

        [SerializeField]
        private Fight.FightManager _fight;

        private LevelManager _levelManager;

        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        void OnTriggerEnter(Collider collider)
        {
            GameObject freeCrowd = collider.gameObject;
            if (!Properties.DoesTypeMatch(freeCrowd, Properties.Type.Free)) return;
            collider.enabled = false;

            CreateNewPlayer(freeCrowd.transform.position, freeCrowd.transform.rotation);
            Destroy(freeCrowd);
        }

        private void CreateNewPlayer(Vector3 position, Quaternion rotation)
        {
            GameObject newPlayer = Instantiate(PlayerPrefab, position, rotation, _levelManager.LevelObject.transform);
            newPlayer.name = PlayerPrefab.name;
            _fight.Players.AddLast(newPlayer);
        }

    }
}