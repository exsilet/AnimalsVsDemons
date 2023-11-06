using System.Collections;
using System.Collections.Generic;
using Data;
using Infrastructure.Service.SaveLoad;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class OpenNewMinions : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private List<MinionView> _upgradeMinionViews;
        [SerializeField] private PlayerMoney _playerMoney;
        [SerializeField] private int _defaultMinionsCount = 1;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _refuseImage;

        private State _heroStats;
        private int _maxMinions;
        private int _countMinions;
        private int _currentCostOfGold;
        private int _currentSoul;
        private int _nextMinion;
        private int _gameLevel;
        private int _avalebleMinions;

        private void Start()
        {
            _maxMinions = _upgradeMinionViews.Count + 1;
            _nextMinion = _countMinions - _defaultMinionsCount;
            OpenMinions();
            SetAvalebleMinions();
        }

        private void Update()
        {
            _currentSoul = _playerMoney.EarnedSouls;
            _currentCostOfGold = _heroStats.PriceNewMinions * (_countMinions);
            _price.text = _currentCostOfGold.ToString();
        }
        
        private void OnEnable()
        {
            foreach (MinionView minionView in _upgradeMinionViews)
            {
                minionView.SellButtonClick += TrySellBuy;
            }
        }

        private void OnDisable()
        {
            foreach (MinionView minionView in _upgradeMinionViews)
            {
                minionView.SellButtonClick -= TrySellBuy;
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroStats = progress.HeroState;
            _countMinions = progress.NumberOfMinions;
            _gameLevel = progress.HeroState.GameLevel;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.NumberOfMinions = _countMinions;
        }

        private void TrySellBuy(MinionView minionView)
        {
            if (_countMinions <= _maxMinions)
            {
                if (_currentSoul <= 0)
                    return;

                if (_currentCostOfGold <= _currentSoul)
                {
                    if (_countMinions < _avalebleMinions)
                    {
                        _playerMoney.BuyOpenNewMinions(_heroStats, _countMinions);
                        _countMinions += 1;
                        minionView.BuyUpgrade();
                    }
                    else
                        StartCoroutine(nameof(RefuseBuy));
                }
            }
        }
        
        private void OpenMinions()
        {
            if (_nextMinion < _upgradeMinionViews.Count) _upgradeMinionViews[_nextMinion].SellButton.interactable = true;

            for (int i = 0; i < _countMinions - _defaultMinionsCount; i++)
            {
                _upgradeMinionViews[i].BuyUpgrade();
            }
        }   
        
        private void SetAvalebleMinions()
        {
            if (_gameLevel == 0) _avalebleMinions = 3;
            else if (_gameLevel == 1) _avalebleMinions = 4;
            else if (_gameLevel == 2) _avalebleMinions = 5;
        }

        private IEnumerator RefuseBuy()
        {
            _refuseImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            _refuseImage.gameObject.SetActive(false);
        }        
    }
}