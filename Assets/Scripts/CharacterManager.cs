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

    private FightManager _fight;
    private LevelManager _levelManager;
    private ColorMixingManager _colorMixing;
    private Animator _animator;

    public LinkedList<GameObject> ThisTeam { get; private set; }
    public LinkedList<GameObject> OtherTeam { get; private set; }

    public bool IsFighting { get; private set; }
    public bool IsDead { get; private set; }

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

    public void SetRunning(bool isRunning)
    {
        _animator.SetBool("running", isRunning);
        if (_fight.IsFightStarted)
        {
            GetComponent<FightBehaviour>().enabled = isRunning;
            if (!isRunning)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        else
        {
            GetComponent<MoveForward>().enabled = isRunning;
        }
    }

    public void SetFighting(bool isFighting)
    {
        _animator.SetBool("fighting", isFighting);
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
            Destroy(Camera.main.GetComponent<MoveForward>());
            _levelManager.EndLevel(isWin: ThisTeam == _fight.Enemies);
        }
    }

    public void MakeCelebrating()
    {
        _animator.SetBool("celebrating", true);
        transform.rotation = Quaternion.Euler(0, 180, 0);
        SetRunning(false);
    }

}
