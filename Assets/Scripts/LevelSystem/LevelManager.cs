using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics.ColorMixing;
using Movement;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {

        public GameObject WinScreen;
        public GameObject LoseScreen;

        public GameObject[] Levels;

        public GameObject LevelObject { get; private set; }
        private int _levelNumber;
        private Vector3 _initialCameraPosition;
        private bool _levelStarted = false;
        private GameObject _currentGUIObject;

        private void Start()
        {
            _levelNumber = 1; // TODO: save/load progress
            _initialCameraPosition = Camera.main.transform.position;
            LoadLevel();
        }

        private ColorMixingManager _colorMixing;
        private CoinsManager _coinsManager;

        public void Construct(ColorMixingManager colorMixing, CoinsManager coinsManager)
        {
            _colorMixing = colorMixing;
            _coinsManager = coinsManager;
        }

        public void EndLevel(bool isWin)
        {
            if (!_levelStarted) return;
            _levelStarted = false;

            if (isWin)
            {
                _levelNumber++;
                _coinsManager.Coins += CoinsManager.CoinsForLevelFinish;
            }

            _currentGUIObject = isWin ? WinScreen : LoseScreen;
            _currentGUIObject.SetActive(true);
        }

        public void LoadLevel()
        {
            if (_levelNumber > Levels.Length)
            {
                _levelNumber = 1;
            }

            Destroy(LevelObject);
            LevelObject = Instantiate(Levels[_levelNumber - 1]);
            LevelObject.name = "Level";

            if (_currentGUIObject) _currentGUIObject.SetActive(false);
            _levelStarted = true;

            Camera.main.transform.position = _initialCameraPosition;
            Camera.main.gameObject.GetComponent<MoveForward>().enabled = true;
            _colorMixing.ResetColor();

            LevelInfo levelInfo = LevelObject.GetComponent<LevelInfo>();
            levelInfo.Player.GetComponent<CharacterManager>().MakePlayer();
        }
    }
}