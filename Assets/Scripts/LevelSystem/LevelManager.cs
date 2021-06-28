using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics.ColorMixing;
using Movement;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {

        public GameObject[] Levels;

        public GameObject LevelObject { get; private set; }
        private int _levelNumber;
        private Vector3 _initialCameraPosition;
        private bool _isLevelStarted = false;

        private void Start()
        {
            _levelNumber = 1; // TODO: save/load progress
            _initialCameraPosition = Camera.main.transform.position;
            LoadLevel();
        }

        private ColorMixingManager _colorMixing;
        private CoinsManager _coinsManager;
        private GUIManager _guiManager;

        public void Construct(ColorMixingManager colorMixing, CoinsManager coinsManager, GUIManager guiManager)
        {
            _colorMixing = colorMixing;
            _coinsManager = coinsManager;
            _guiManager = guiManager;
        }

        public void EndLevel(bool isWin)
        {
            if (!_isLevelStarted) return;
            _isLevelStarted = false;

            if (isWin)
            {
                _levelNumber++;
                _coinsManager.Coins += _coinsManager.CoinsForLevelFinish;
            }

            _guiManager.ShowScreen(isWin ? _guiManager.WinScreen : _guiManager.LoseScreen);
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

            Camera.main.transform.position = _initialCameraPosition;
            Camera.main.gameObject.GetComponent<MoveForward>().enabled = true;

            LevelInfo levelInfo = LevelObject.GetComponent<LevelInfo>();
            levelInfo.Player.GetComponent<CharacterManager>().MakePlayer();

            _guiManager.ShowScreen(_guiManager.PlayControls);
            _colorMixing.ResetColor();
            _isLevelStarted = true;
        }

    }
}