using System;
using System.Collections.Generic;
using Data;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using Player;
using Tower;
using UI.Element;
using UnityEngine;

namespace UI.Forms
{
    public class ShopWindow : BaseWindow, ISavedProgressReader
    {
        [SerializeField] private List<TowerStaticData> _items;
        [SerializeField] private List<TowerView> _towerViewPrefabs;
        [SerializeField] private OpenPanelMinions _panelMinions;

        private PlayerMoney _playerMoney;
        private Inventory _inventory;
        private int _currentCostOfGold;
        private int _currentMoney;
        private readonly List<TowerView> _towerViews = new();
        
        public event Action<bool> Opened;
        private bool _isOpen;
        private int _countMinions;
        private readonly int _defaultMinionsCount = 1;

        public void Construct(PlayerMoney playerMoney, Inventory inventory)
        {
            _playerMoney = playerMoney;
            _inventory = inventory;
        }
        
        public void LoadProgress(PlayerProgress progress)
            => _countMinions = progress.NumberOfMinions;

        public void Inactive()
        {
            _isOpen = false;
            Opened?.Invoke(_isOpen);
        }

        private void Start()
        {
            CloseMinions();
            OpenMinions();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            for (int i = 0; i < _items.Count; i++)
            {
                _towerViews.Add(_towerViewPrefabs[i]);
                _towerViewPrefabs[i].Initialize(_items[i]);
            }
        }

        private void Update()
            => _currentMoney = _playerMoney.CurrentSoul;

        private void OnEnable()
        {
            _isOpen = true;
            Opened?.Invoke(_isOpen);

            foreach (TowerView view in _towerViews)
            {
                view.SellButtonClick += TrySellBuy;
                view.Highlighted += _panelMinions.Show;
            }
        }

        private void OnDisable()
        {
            foreach (TowerView view in _towerViews)
            {
                view.SellButtonClick -= TrySellBuy;
                view.Highlighted -= _panelMinions.Show;
            }
        }

        private void TrySellBuy(TowerStaticData data, TowerView view)
        {
            _currentCostOfGold = data.Price;

            if (_currentMoney <= 0)
                return;

            if (_currentCostOfGold <= _currentMoney)
            {
                _playerMoney.BuyTower(data);
                _inventory.BuyMinions(data);
                _inventory.SpawnMinions();
                gameObject.SetActive(false);
            }
        }
        
        private void CloseMinions()
        {
            foreach (TowerView towerView in _towerViewPrefabs)
            {
                towerView.CloseMinions();
            }
        }
        
        private void OpenMinions()
        {
            for (int i = 0; i <= _countMinions - _defaultMinionsCount; i++)
            {
                _towerViewPrefabs[i].OpenMinions();
            }
        }
    }
}