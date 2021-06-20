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
    private Vector3 _initialCameraPosition;

    private void Start()
    {
        _levelNumber = 1; // TODO: save/load progress
        _initialCameraPosition = Camera.main.transform.position;
        LoadLevelButton();
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
            if (_levelNumber > Levels.Length)
            {
                EndLevelStatusText.text = "All Levels Complete!";
                EndLevelButtonText.text = "Start from the beginning";
            }
            else
            {
                EndLevelStatusText.text = "Level Complete!";
                EndLevelButtonText.text = "Next";
            }
        }
        else
        {
            EndLevelStatusText.text = "Try again";
            EndLevelButtonText.text = "Retry";
        }
        EndLevelScreen.SetActive(true);
    }

    public void LoadLevelButton()
    {
        // check if we need to start over
        if (_levelNumber > Levels.Length)
        {
            _levelNumber = 1;
        }

        // create new level
        Destroy(LevelObject);
        LevelObject = Instantiate(Levels[_levelNumber - 1]);
        LevelObject.name = "Level";
        EndLevelScreen.SetActive(false);

        // get info
        LevelInfo levelInfo = LevelObject.GetComponent<LevelInfo>();
        GameObject player = levelInfo.Player;

        // setup player's material
        _colorMixing.ResetColor();
        player.GetComponent<Renderer>().sharedMaterial = _colorMixing.PlayerMaterial;

        // fix camera
        Camera.main.transform.position = _initialCameraPosition;
        Camera.main.gameObject.AddComponent<Movement.MoveForward>();
    }
}