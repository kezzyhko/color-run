using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Fight
{
    public class FightBehaviour : MonoBehaviour
    {

        public GameObject Target = null;
        public FightManager Fight;
        public bool ShouldDestroy;

        private const float SecondsToFight = 2.0f;

        private Rigidbody _rigidbody;
        private CharacterManager _characterManager;
        private CharacterManager _targetManager;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _characterManager = GetComponent<CharacterManager>();

            FindTarget();
            _characterManager.SetRunning(true);
        }

        void Update()
        {
            if (_targetManager == null || _targetManager.IsDead)
            {
                // check for win
                if (_characterManager.OtherTeam.Count == 0)
                {
                    _characterManager.MakeCelebrating();
                    return;
                }

                FindTarget();
            }

            if (!_characterManager.IsFighting)
            {
                Vector3 direction = Target.transform.position - transform.position;
                direction.Normalize();
                _rigidbody.velocity = direction * _characterManager.Speed;
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }

        private void FindTarget()
        {
            GameObject closestOther = null;
            float closestDistance = float.PositiveInfinity;
            foreach (GameObject other in _characterManager.OtherTeam)
            {
                float distance = Vector3.Distance(other.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestOther = other;
                    closestDistance = distance;
                }
            }
            Target = closestOther;
            _targetManager = Target.GetComponent<CharacterManager>();
        }

        private IEnumerator OnTriggerStay(Collider collider)
        {
            if (collider.gameObject != Target) yield break;
            if (_characterManager.IsFighting) yield break;

            _characterManager.SetFighting(true);
            yield return new WaitForSeconds(SecondsToFight);
            if (_characterManager.IsDead) yield break;
            Target.GetComponent<CharacterManager>().MakeDead();
            _characterManager.SetFighting(false);
        }
    }
}