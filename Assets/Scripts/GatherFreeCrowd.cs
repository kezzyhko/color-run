using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public ColorMixing ColorMixing;
    public GameObject PlayerPrefab;

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject freeCrowd = collider.gameObject;
        Properties props = freeCrowd.GetComponent<Properties>();
        if (props == null || props.ObjectType != Properties.Type.FreeCrowd) return;

        // create new player
        Destroy(freeCrowd);
        GameObject newPlayer = Instantiate(PlayerPrefab, freeCrowd.transform.position, freeCrowd.transform.rotation);
        newPlayer.GetComponent<GatherFreeCrowd>().ColorMixing = ColorMixing;
        newPlayer.GetComponent<GatherFreeCrowd>().PlayerPrefab = PlayerPrefab;
        newPlayer.GetComponent<ObstacleCollision>().ColorMixing = ColorMixing;
        ColorMixing.Players.AddLast(newPlayer);

        // set color
        Color playerColor = gameObject.GetComponent<Renderer>().material.color;
        newPlayer.GetComponent<Renderer>().material.color = playerColor;
    }

}