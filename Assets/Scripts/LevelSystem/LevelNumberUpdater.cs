using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelNumberUpdater : MonoBehaviour
    {

        private LevelManager _levelManager;
        private Text _text;

        public void Construct(LevelManager levelManager, Text text)
        {
            _levelManager = levelManager;
            _text = text;
        }

        void Start()
        {
            _levelManager.LevelNumberChanged += UpdateLevelNumber;
            UpdateLevelNumber(_levelManager.LevelNumber);
        }

        private void OnDestroy()
        {
            _levelManager.LevelNumberChanged -= UpdateLevelNumber;
        }

        private void UpdateLevelNumber(int newLevel)
        {
            _text.text = $"LEVEL {newLevel}";
        }

    }
}