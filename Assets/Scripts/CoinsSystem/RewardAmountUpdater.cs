using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CoinsSystem
{
    public class RewardAmountUpdater : MonoBehaviour
    {

        private CoinsManager _coinsSystem;
        private Text _text;

        public void Construct(CoinsManager coinsSystem, Text text)
        {
            _coinsSystem = coinsSystem;
            _text = text;
        }

        private void Start()
        {
            _text.text = _coinsSystem.CoinsForLevelFinish.ToString();
        }

    }
}