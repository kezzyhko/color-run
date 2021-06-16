using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{

    public GameObject InitialPlayer;
    public GameObject[] InitialEnemies;

    public LinkedList<GameObject> Players = new LinkedList<GameObject>();
    public LinkedList<GameObject> Enemies;

    private bool _isFightStarted;

    private void Start()
    {
        Players.AddLast(InitialPlayer);
        Enemies = new LinkedList<GameObject>(InitialEnemies);
    }

    void OnTriggerEnter(Collider collider)
    {
        // execute only once
        if (_isFightStarted) return;
        _isFightStarted = true;

        // stop moving forward
        foreach (GameObject player in Players)
        {
            Destroy(player.GetComponent<MoveForward>());
        }
        Destroy(Camera.main.GetComponent<MoveForward>());

        // add fighting logic
        AddScript(Players, true);
        AddScript(Enemies, false);
    }

    private void AddScript(LinkedList<GameObject> team, bool shouldDestroy)
    {
        foreach (GameObject character in team)
        {
            MoveTowardsTarget moveTowardsTarget = character.AddComponent<MoveTowardsTarget>();
            moveTowardsTarget.Fight = this;
            moveTowardsTarget.ShouldDestroy = shouldDestroy;
        }
    }
}