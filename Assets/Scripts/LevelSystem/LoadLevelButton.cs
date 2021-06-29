using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LoadLevelButton : MonoBehaviour
    {
        private LevelManager _levelManager;

        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(_levelManager.LoadLevel);
        }

        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(_levelManager.LoadLevel);
        }
    }
}