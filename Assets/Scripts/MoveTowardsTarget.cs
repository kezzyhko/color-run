using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{

    public GameObject Target = null;
    public Fight Fight;
    public bool ShouldDestroy;

    public float Speed = 1.0f;
    public float MinDistance = 1.5f;


    private LinkedList<GameObject> _thisTeam;
    private LinkedList<GameObject> _otherTeam;
    private Rigidbody _rigidbody;

    private void Start()
    {
        if (GetComponent<Properties>().ObjectType == Properties.Type.Player)
        {
            _thisTeam = Fight.Players;
            _otherTeam = Fight.Enemies;
        }
        else
        {
            _thisTeam = Fight.Enemies;
            _otherTeam = Fight.Players;
        }
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // check for win
        if (_otherTeam.Count == 0)
        {
            _rigidbody.velocity = Vector3.zero;
            Destroy(this);
            return;
        }

        // choose target
        if (Target == null)
        {
            GameObject closestOther = null;
            float closestDistance = float.PositiveInfinity;
            foreach (GameObject other in _otherTeam)
            {
                float distance = Vector3.Distance(other.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestOther = other;
                    closestDistance = distance;
                }
            }
            Target = closestOther;
        }

        Vector3 direction = Target.transform.position - transform.position;
        if (direction.magnitude > MinDistance)
        {
            // move to target
            _rigidbody.velocity = direction.normalized;
        }
        else
        {
            // fight with target
            if (ShouldDestroy)
            {
                Fight.RemoveCharacter(Target, _otherTeam);
                Fight.RemoveCharacter(gameObject, _thisTeam);
            }
        }
    }
}