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
        // get info
        //GameObject player = collider.gameObject;
        //Properties props = player.GetComponent<Properties>();
        //if (props == null || props.ObjectType != Properties.Type.Player) return;

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
            character.AddComponent<MoveTowardsTarget>();
            character.GetComponent<MoveTowardsTarget>().Fight = this;
            character.GetComponent<MoveTowardsTarget>().ShouldDestroy = shouldDestroy;
        }
    }
}