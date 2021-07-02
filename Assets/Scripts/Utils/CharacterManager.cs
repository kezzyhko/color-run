using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics.Fight;
using Mechanics.Collisions;
using Mechanics.ColorMixing;
using LevelSystem;
using static Utils.PropertiesHelper.ObjectType;
using static Utils.ColorHelper;

namespace Utils
{
    public class CharacterManager : MonoBehaviour
    {

        public float Speed = 2.0f;
        public float RotationSpeed = 90.0f;

        private FightManager _fight;
        private LevelManager _levelManager;
        private ColorMixingManager _colorMixing;
        private Animator _animator;

        public LinkedList<GameObject> ThisTeam { get; private set; }
        public LinkedList<GameObject> OtherTeam { get; private set; }

        public bool IsFighting { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsCelebrating { get; private set; }

        public void Construct(LevelInfo levelInfo, LevelManager levelManager, ColorMixingManager colorMixing, Animator animator)
        {
            _fight = levelInfo.FightTrigger.GetComponent<FightManager>();
            _levelManager = levelManager;
            _colorMixing = colorMixing;
            _animator = animator;
        }

        private void Start()
        {
            _animator.SetFloat("offset", Random.Range(0.0f, 1.0f));

            if (gameObject.DoesTypeMatch(Enemy))
            {
                ThisTeam = _fight.Enemies;
                OtherTeam = _fight.Players;
            }
            else
            {
                ThisTeam = _fight.Players;
                OtherTeam = _fight.Enemies;
            }
        }

        public void MakePlayer()
        {
            GetComponent<Properties>().Type = Player;
            SetRunning(true);
            _fight.Players.AddLast(gameObject);
            gameObject.SetObjectMaterial(_colorMixing.PlayerMaterial);

            LinkedList<GameObject> newAdjacentCrowd = GetComponent<GatherFreeCrowd>().AdjacentFreeCrowd;
            foreach (GameObject adjacentFree in newAdjacentCrowd)
            {
                if (!adjacentFree.DoesTypeMatch(Free)) continue;
                adjacentFree.GetComponent<CharacterManager>().MakePlayer();
            }
        }

        public void RotateTowards(Vector3 direction)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * RotationSpeed);
        }

        public void SetRunning(bool isRunning)
        {
            _animator.SetBool("running", isRunning);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.constraints = isRunning ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
            rigidbody.velocity = isRunning ? Vector3.forward * Speed : Vector3.zero;
        }

        public void SetFighting(bool isFighting)
        {
            _animator.SetBool("fighting", isFighting);
            if (!IsCelebrating) SetRunning(!isFighting);
            IsFighting = isFighting;
        }

        public void MakeDead()
        {
            _animator.SetBool("dead", true);
            IsDead = true;
            SetRunning(false);
            GetComponent<Rigidbody>().detectCollisions = false;
            gameObject.SetObjectMaterial(Instantiate(gameObject.GetObjectMaterial()));

            ThisTeam.Remove(gameObject);
            if (ThisTeam.Count == 0)
            {
                _levelManager.EndLevel(isWin: ThisTeam == _fight.Enemies, isOnFight: _fight.IsFightStarted);
            }
        }

        public void MakeCelebrating()
        {
            _animator.SetBool("celebrating", true);
            IsCelebrating = true;
            SetRunning(false);
        }

    }
}