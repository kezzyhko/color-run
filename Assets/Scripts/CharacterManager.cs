using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using ColorUtils;
using Mechanics.Fight;
using Mechanics.Collisions;
using Mechanics.ColorMixing;

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

    public void Construct(LevelInfo levelInfo, LevelManager levelManager, ColorMixingManager colorMixing)
    {
        _fight = levelInfo.FightTrigger.GetComponent<FightManager>();
        _levelManager = levelManager;
        _colorMixing = colorMixing;

        _animator = GetComponentInChildren<Animator>();
        _animator.SetFloat("offset", Random.Range(0.0f, 1.0f));

        if (Properties.DoesTypeMatch(gameObject, Properties.Type.Enemy))
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
        GetComponent<Properties>().ObjectType = Properties.Type.Player;
        SetRunning(true);
        ColorHelper.SetObjectMaterial(gameObject, _colorMixing.PlayerMaterial);

        _fight.Players.AddLast(gameObject);

        LinkedList<GameObject> newAdjacentCrowd = GetComponent<GatherFreeCrowd>().AdjacentFreeCrowd;
        foreach (GameObject adjacentFree in newAdjacentCrowd)
        {
            if (!Properties.DoesTypeMatch(adjacentFree, Properties.Type.Free)) continue;
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
        rigidbody.velocity = isRunning ? Vector3.forward * Speed : Vector3.zero;
        rigidbody.constraints = isRunning ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
    }

    public void SetFighting(bool isFighting)
    {
        _animator.SetBool("fighting", isFighting);
        SetRunning(!isFighting);
        IsFighting = isFighting;
    }

    public void MakeDead()
    {
        _animator.SetBool("dead", true);
        IsDead = true;
        GetComponent<Rigidbody>().detectCollisions = false;
        SetRunning(false);

        ThisTeam.Remove(gameObject);
        if (ThisTeam.Count == 0)
        {
            Camera.main.GetComponent<MoveForward>().enabled = false;
            _levelManager.EndLevel(isWin: ThisTeam == _fight.Enemies);
        }
    }

    public void MakeCelebrating()
    {
        _animator.SetBool("celebrating", true);
        IsCelebrating = true;
        SetRunning(false);
    }

}
