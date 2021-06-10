using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject newPlayer = collider.gameObject;
        Properties props = newPlayer.GetComponent<Properties>();
        if (props == null || props.type != Properties.Type.FreeCrowd) return;

        // change team
        props.type = Properties.Type.Player;

        // change color
        Color playerColor = gameObject.GetComponent<Renderer>().material.color;
        newPlayer.GetComponent<Renderer>().material.color = playerColor;

        // add necessary scripts
        newPlayer.AddComponent<PlayerControl>();
        newPlayer.AddComponent<GatherFreeCrowd>();
        newPlayer.AddComponent<ObstacleCollision>();
        newPlayer.AddComponent<Rigidbody>();
        newPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }

}
