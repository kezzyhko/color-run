using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics.ColorMixing;
using Movement;
using CoinsSystem;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {

        public GameObject[] Levels;

        private int _levelNumber;
        public event System.Action<int> LevelNumberChanged;
        public int LevelNumber
        {
            get
            {
                return _levelNumber;
            }
            set
            {
                _levelNumber = value;
                if (LevelNumberChanged != null) LevelNumberChanged(value);
            }
        }

        private GameObject _levelObject;
        private Vector3 _initialCameraPosition;
        private bool _isLevelEnded = false;
        private CharacterManager _playerManager;

        private void Start()
        {
            LevelNumber = 1; // TODO: save/load progress
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
            if (_isLevelEnded) return;
            _isLevelEnded = true;

            if (isWin)
            {
                LevelNumber++;
                _coinsManager.Coins += _coinsManager.CoinsForLevelFinish;
            }

            _guiManager.ShowScreen(isWin ? _guiManager.WinScreen : _guiManager.LoseScreen);
            _colorMixing.AbortSelection();
        }

        public void LoadLevel()
        {
            if (LevelNumber > Levels.Length)
            {
                LevelNumber = 1;
            }

            Destroy(_levelObject);
            _levelObject = Instantiate(Levels[LevelNumber - 1]);
            _levelObject.name = "Level";

            LevelInfo levelInfo = _levelObject.GetComponent<LevelInfo>();
            _playerManager = levelInfo.Player.GetComponent<CharacterManager>();
            _playerManager.MakePlayer();
            _playerManager.SetRunning(false);

            Camera.main.transform.position = _initialCameraPosition;
            _guiManager.ShowScreen(_guiManager.Menu);
            _colorMixing.ResetColor();
            _isLevelEnded = false;
        }

        public void StartLevel()
        {
            _playerManager.SetRunning(true);
            Camera.main.gameObject.GetComponent<MoveForward>().enabled = true;
            _guiManager.ShowScreen(_guiManager.PlayControls);
        }

    }
}