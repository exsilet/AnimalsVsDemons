using Data;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMoney : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private int _currentSoul;
        [SerializeField] private int _startSoulIncreaseAmount;
        [SerializeField] private int _rewardRete;

        private int _earnedSouls;
        public event UnityAction<int> CurrentSoulChanged;

        public int CurrentSoul => _currentSoul;
        public int EarnedSouls => _earnedSouls;        

        private void Start()
            => CurrentSoulChanged?.Invoke(_currentSoul);

        public void BuyTower(TowerStaticData data)
        {
            if (_currentSoul >= data.Price)
            {
                _currentSoul -= data.Price;
                CurrentSoulChanged?.Invoke(_currentSoul);
            }
        }

        public void BuyUpgradeHero(State heroState, int stepUpgrade)
        {
            if (_earnedSouls > 0)
            {
                _earnedSouls -= heroState.PriceLevel * (stepUpgrade);
                CurrentSoulChanged?.Invoke(_earnedSouls);
            }
        }

        public void BuyUpgradeHeroSpell(State heroState, int stepUpgrade)
        {
            if (_earnedSouls > 0)
            {
                _earnedSouls -= heroState.PriceSpell * (stepUpgrade);
                CurrentSoulChanged?.Invoke(_earnedSouls);
            }
        }

        public void BuyOpenNewMinions(State heroState, int stepUpgrade)
        {
            if (_earnedSouls > 0)
            {
                _earnedSouls -= heroState.PriceNewMinions * (stepUpgrade);
                CurrentSoulChanged?.Invoke(_earnedSouls);
            }
        }

        public void SellMinions(TowerStaticData data)
        {
            _currentSoul += data.Price;
            CurrentSoulChanged?.Invoke(_currentSoul);
        }

        public void AddMoney(int reward)
        {
            _currentSoul += reward;
            CurrentSoulChanged?.Invoke(_currentSoul);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            _earnedSouls += _currentSoul;
            progress.CurrentSoul = _earnedSouls;
        }

        public void LoadProgress(PlayerProgress progress)
            => _earnedSouls = progress.CurrentSoul;
    }
}