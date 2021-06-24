using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mechanics.ColorMixing;
using Movement;
using ColorUtils;

public class LevelManager : MonoBehaviour
{

    public GameObject EndLevelScreen;
    public Text EndLevelStatusText;
    public Text EndLevelButtonText;

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

    private ColorMixingManager _colorMixing;

    public void Construct(ColorMixingManager colorMixing)
    {
        _colorMixing = colorMixing;
    }

    public void EndLevel(bool isWin)
    {
        if (EndLevelScreen.activeSelf) return;

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
        if (_levelNumber > Levels.Length)
        {
            _levelNumber = 1;
        }

        Destroy(LevelObject);
        LevelObject = Instantiate(Levels[_levelNumber - 1]);
        LevelObject.name = "Level";
        EndLevelScreen.SetActive(false);

        Camera.main.transform.position = _initialCameraPosition;
        Camera.main.gameObject.AddComponent<MoveForward>();
        _colorMixing.ResetColor();

        LevelInfo levelInfo = LevelObject.GetComponent<LevelInfo>();
        levelInfo.Player.GetComponent<CharacterManager>().MakePlayer();
    }
}