using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Fight
{
    public class FightBehaviour : MonoBehaviour
    {

        private const float MinSecondsToFight = 3.0f;
        private const float MaxSecondsToFight = 5.0f;

        public GameObject Target;
        private CharacterManager _targetManager;

        private Rigidbody _rigidbody;
        private CharacterManager _characterManager;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _characterManager = GetComponent<CharacterManager>();

            FindTarget();
            _characterManager.SetRunning(true);
        }

        void Update()
        {
            if (_characterManager.IsDead) return;

            if (_characterManager.IsCelebrating)
            {
                _characterManager.RotateTowards(Vector3.back);
                return;
            }

            if (_targetManager == null || _targetManager.IsDead)
            {
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
                _characterManager.RotateTowards(direction);
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
            yield return new WaitForSeconds(Random.Range(MinSecondsToFight, MaxSecondsToFight));
            if (_characterManager.IsDead) yield break;
            _targetManager.MakeDead();
            _characterManager.SetFighting(false);
        }
    }
}