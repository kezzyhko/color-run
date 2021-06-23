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
    private LinkedList<GameObject> _thisTeam;
    private LinkedList<GameObject> _otherTeam;
    private Animator _animator;

    public void Construct(LevelInfo levelInfo, LevelManager levelManager, ColorMixingManager colorMixing)
    {
        _fight = levelInfo.FightTrigger.GetComponent<FightManager>();
        _levelManager = levelManager;
        _colorMixing = colorMixing;

        _animator = GetComponent<Properties>().Animator;
        _animator.SetFloat("offset", Random.Range(0.0f, 1.0f));

        if (Properties.DoesTypeMatch(gameObject, Properties.Type.Enemy))
        {
            _thisTeam = _fight.Enemies;
            _otherTeam = _fight.Players;
        }
        else
        {
            _thisTeam = _fight.Players;
            _otherTeam = _fight.Enemies;
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
        GetComponent<MoveForward>().enabled = isRunning;
    }

    public void MakeDead()
    {
        _animator.SetBool("dead", true);
        GetComponent<Rigidbody>().detectCollisions = false;
        SetRunning(false);

        _thisTeam.Remove(gameObject);
        if (_thisTeam.Count == 0)
        {
            Destroy(Camera.main.GetComponent<MoveForward>());
            _levelManager.EndLevel(isWin: _thisTeam == _fight.Enemies);
        }
    }

}
