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
    public float DestroyDelay = 1.0f;


    private LinkedList<GameObject> _thisTeam;
    private LinkedList<GameObject> _otherTeam;

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
    }

    void Update()
    {
        // check for win
        if (_otherTeam.Count == 0)
        {
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
            direction = Vector3.ClampMagnitude(direction, Time.deltaTime * Speed);
            transform.position += direction;
        }
        else
        {
            // fight with target
            if (ShouldDestroy)
            {
                RemoveCharacter(gameObject, _thisTeam);
                RemoveCharacter(Target, _otherTeam);
            }
        }
    }

    private void RemoveCharacter(GameObject character, LinkedList<GameObject> team)
    {
        team.Remove(character);
        Destroy(character);
    }
}
