using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public ColorMixing ColorMixing;

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject newPlayer = collider.gameObject;
        Properties props = newPlayer.GetComponent<Properties>();
        if (props == null || props.ObjectType != Properties.Type.FreeCrowd) return;

        // change team
        props.ObjectType = Properties.Type.Player;
        ColorMixing.Players.AddLast(newPlayer);

        // change color
        Color playerColor = gameObject.GetComponent<Renderer>().material.color;
        newPlayer.GetComponent<Renderer>().material.color = playerColor;

        // add necessary scripts
        newPlayer.AddComponent<PlayerControl>();
        newPlayer.AddComponent<GatherFreeCrowd>();
        newPlayer.GetComponent<GatherFreeCrowd>().ColorMixing = ColorMixing;
        newPlayer.AddComponent<ObstacleCollision>();
        newPlayer.GetComponent<ObstacleCollision>().ColorMixing = ColorMixing;
        newPlayer.AddComponent<Rigidbody>();
        newPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }

}