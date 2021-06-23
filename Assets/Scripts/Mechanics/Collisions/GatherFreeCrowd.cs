using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using ColorUtils;
using Mechanics.Fight;

namespace Mechanics.Collisions
{
    public class GatherFreeCrowd : MonoBehaviour
    {

        private LinkedList<GameObject> _adjacentFreeCrowd = new LinkedList<GameObject>();

        private LevelInfo _levelInfo;

        public void Construct(LevelInfo levelInfo)
        {
            _levelInfo = levelInfo;
        }

        void OnTriggerEnter(Collider collider)
        {
            GameObject free = collider.gameObject;
            if (!Properties.DoesTypeMatch(free, Properties.Type.Free)) return;
            if (Properties.DoesTypeMatch(gameObject, Properties.Type.Free))
            {
                _adjacentFreeCrowd.AddLast(free);
                return;
            }

            AddNewPlayer(free);
        }

        private void AddNewPlayer(GameObject newPlayer)
        {
            newPlayer.GetComponent<Properties>().ObjectType = Properties.Type.Player;
            newPlayer.GetComponent<MoveForward>().enabled = true;
            ColorHelper.SetObjectMaterial(newPlayer, ColorHelper.GetObjectMaterial(gameObject));

            FightManager fight = _levelInfo.FightTrigger.GetComponent<FightManager>();
            fight.Players.AddLast(newPlayer);

            LinkedList<GameObject> newAdjacentCrowd = newPlayer.GetComponent<GatherFreeCrowd>()._adjacentFreeCrowd;
            foreach (GameObject adjacentFree in newAdjacentCrowd)
            {
                if (!Properties.DoesTypeMatch(adjacentFree, Properties.Type.Free)) continue;
                AddNewPlayer(adjacentFree);
            }
        }

    }
}