using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public GameObject PlayerPrefab;

    [SerializeField]
    private Fight _fight;

    private GUIManager _guiManager;

    public void Construct(GUIManager guiManager)
    {
        _guiManager = guiManager;
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
        newPlayer.transform.parent = _guiManager.LevelObject.transform;
        newPlayer.GetComponent<GatherFreeCrowd>()._guiManager = _guiManager;
        Destroy(freeCrowd);
        _fight.Players.AddLast(newPlayer);
    }

}