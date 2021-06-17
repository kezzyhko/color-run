using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject EndLevelScreen;
    public UnityEngine.UI.Text EndLevelStatusText;
    public UnityEngine.UI.Text EndLevelButtonText;

    public GameObject[] Levels;

    public GameObject LevelObject { get; private set; }
    private int _levelNumber;

    private void Start()
    {
        _levelNumber = 1; // TODO: save/load progress
        LoadLevel();
    }

    private ColorMixing _colorMixing;

    public void Construct(ColorMixing colorMixing)
    {
        _colorMixing = colorMixing;
    }

    public void EndLevel(bool isWin)
    {
        if (isWin)
        {
            _levelNumber++;
            EndLevelStatusText.text = "Level Complete!";
            EndLevelButtonText.text = "Next";
        }
        else
        {
            EndLevelStatusText.text = "Try again";
            EndLevelButtonText.text = "Retry";
        }
        EndLevelScreen.SetActive(true);
    }

    public void LoadLevel()
    {
        Destroy(LevelObject);
        LevelObject = Instantiate(Levels[_levelNumber - 1]);
        LevelObject.name = "Level";
        EndLevelScreen.SetActive(false);

        // get info
        LevelInfo levelInfo = LevelObject.GetComponent<LevelInfo>();
        GameObject player = levelInfo.Player;

        // setup player's material
        _colorMixing.PlayerMaterial.color = Color.black;
        player.GetComponent<Renderer>().sharedMaterial = _colorMixing.PlayerMaterial;

        // fix camera
        Camera.main.transform.position = player.transform.position + new Vector3(0, 7.2f, -5);
        Camera.main.gameObject.AddComponent<MoveForward>();
    }
}
