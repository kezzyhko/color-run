using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public GameObject PlayerPrefab;

    [SerializeField]
    private Fight _fight;

    private LevelManager _levelManager;

    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject freeCrowd = collider.gameObject;
        Properties props = freeCrowd.GetComponent<Properties>();
        if (props == null || props.ObjectType != Properties.Type.Free) return;
        collider.enabled = false;

        // create new player
        GameObject newPlayer = Instantiate(PlayerPrefab, freeCrowd.transform.position, freeCrowd.transform.rotation);
        newPlayer.name = PlayerPrefab.name;
        newPlayer.transform.parent = _levelManager.LevelObject.transform;
        newPlayer.GetComponent<GatherFreeCrowd>()._levelManager = _levelManager;
        Destroy(freeCrowd);
        _fight.Players.AddLast(newPlayer);
    }

}