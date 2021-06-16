using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public GameObject PlayerPrefab;

    private Fight _fight;

    public void Construct(Fight fight)
    {
        _fight = fight;
    }

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject freeCrowd = collider.gameObject;
        Properties props = freeCrowd.GetComponent<Properties>();
        if (props == null || props.ObjectType != Properties.Type.Free) return;

        // create new player
        Destroy(freeCrowd);
        GameObject newPlayer = Instantiate(PlayerPrefab, freeCrowd.transform.position, freeCrowd.transform.rotation);
        _fight.Players.AddLast(newPlayer);
    }

}