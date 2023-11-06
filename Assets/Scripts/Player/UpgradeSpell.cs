using System.Collections.Generic;
using Data;
using Infrastructure.Service.SaveLoad;
using TMPro;
using UI.Element;
using UnityEngine;

namespace Player
{
    public class UpgradeSpell : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private List<SpellView> _upgradeSpellViews;
        [SerializeField] private PlayerMoney _playerMoney;
        [SerializeField] private int _defaultSpellCount = 2;
        [SerializeField] private TMP_Text _price;

        private State _heroStats;
        private int _currentCostOfGold;
        private int _maxLevelUpgrade;
        private int _currentSoul;
        private int _spellAmount;
        private int _nextBook;

        public void LoadProgress(PlayerProgress progress)
        {
            _heroStats = progress.HeroState;
            _spellAmount = _heroStats.SpellAmount;
        }

        public void UpdateProgress(PlayerProgress progress)
            => progress.HeroState.SpellAmount = _spellAmount;

        private void Start()
        {
            _maxLevelUpgrade = _upgradeSpellViews.Count + _defaultSpellCount;
            _nextBook = _spellAmount - _defaultSpellCount;

            OpenSpellView();
        }

        private void Update()
        {
            _currentSoul = _playerMoney.EarnedSouls;
            _currentCostOfGold = _heroStats.PriceSpell * (_spellAmount);
            _price.text = _currentCostOfGold.ToString();
        }

        private void OnEnable()
        {
            foreach (SpellView upgradeSpell in _upgradeSpellViews)
            {
                upgradeSpell.SellButtonClick += TrySellBuy;
            }
        }

        private void OnDisable()
        {
            foreach (SpellView upgradeSpell in _upgradeSpellViews)
            {
                upgradeSpell.SellButtonClick -= TrySellBuy;
            }
        }

        private void OpenSpellView()
        {
            if (_nextBook < _upgradeSpellViews.Count) _upgradeSpellViews[_nextBook].SellButton.interactable = true;

            for (int i = 0; i < _spellAmount - _defaultSpellCount; i++)
            {
                _upgradeSpellViews[i].BuyUpgrade();
            }
        }

        private void TrySellBuy(SpellView spellView)
        {
            if (_spellAmount <= _maxLevelUpgrade)
            {
                if (_currentSoul <= 0)
                    return;

                if (_currentCostOfGold <= _currentSoul)
                {
                    _playerMoney.BuyUpgradeHeroSpell(_heroStats, _spellAmount);
                    _spellAmount += 1;
                    spellView.BuyUpgrade();
                }
            }
        }
    }
}