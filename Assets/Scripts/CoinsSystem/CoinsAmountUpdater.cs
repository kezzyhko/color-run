using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CoinsSystem
{
    public class CoinsAmountUpdater : MonoBehaviour
    {

        private CoinsManager _coinsSystem;
        private Text _text;

        public void Construct(CoinsManager coinsSystem, Text text)
        {
            _coinsSystem = coinsSystem;
            _text = text;
        }

        void Start()
        {
            _coinsSystem.CoinsAmountChanged += UpdateCoinsAmount;
            UpdateCoinsAmount(_coinsSystem.Coins);
        }

        private void OnDestroy()
        {
            _coinsSystem.CoinsAmountChanged -= UpdateCoinsAmount;
        }

        private void UpdateCoinsAmount(int newCoins)
        {
            _text.text = newCoins.ToString();
        }

    }
}