using System.Collections.Generic;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using Player;
using TMPro;
using UI.Element;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Forms
{
    public class UpgradeMinions : BaseWindow
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private List<TowerStaticData> _data;
        [SerializeField] private OpenPanelMinions _panelMinions;
        [SerializeField] private TMP_Text _price;       

        private TowerStaticData _currentData;
        private TowerStaticData _nextUpgrade;
        private PlayerMoney _playerMoney;
        private Inventory _inventory;
        private int _currentCostOfGold;
        private int _currentMoney;
        private int _maxLevel = 3;

        public OpenPanelMinions PanelMinions => _panelMinions;

        public void Construct(PlayerMoney playerMoney, Inventory inventory)
        {
            _playerMoney = playerMoney;
            _inventory = inventory;
        }

        public void MaxLevelMinions(TowerStaticData data)
        {
            _upgradeButton.interactable = data.Level != _maxLevel;
        }

        public void UpgradeData(TowerStaticData data)
        {
            foreach (TowerStaticData staticData in _data)
            {
                if (staticData.TowerTypeID == data.TowerTypeID)
                {
                    _currentData = staticData;
                }

                if (staticData.TowerTypeID == data.TowerTypeID+1)
                {
                    _nextUpgrade = staticData;
                }
            }
        }

        public void ShowMinions(TowerStaticData data)
        {
            foreach (TowerStaticData staticData in _data)
            {
                if (staticData.TowerTypeID == data.TowerTypeID)
                {
                    _panelMinions.Show(staticData);
                    _price.text = _nextUpgrade.Price.ToString();
                }
            }
        }

        public void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
            _price.text = _nextUpgrade.Price.ToString();
        }
        
        public void ClosePanel(GameObject panel)
        {
            panel.SetActive(false);
        }

        private void Update()
        {
            _currentMoney = _playerMoney.CurrentSoul;
        }

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(UpdateBuy);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(UpdateBuy);
        }

        private void UpdateBuy()
        {
            TryBuy(_nextUpgrade);            
        }

        private void TryBuy(TowerStaticData data)
        {
            _currentCostOfGold = data.Price;

            if (_currentMoney <= 0)
                return;

            if (_currentCostOfGold <= _currentMoney)
            {
                _inventory.CurrentData(_currentData);
                _inventory.SellMinions(_currentData);
                _playerMoney.BuyTower(data);
                _inventory.BuyMinions(data);
                _inventory.SpawnMinions();
                data.ResetHP();
                gameObject.SetActive(false);
            }
        }

        private void TrySell(TowerStaticData data)
        {
            if (data != null)
            {
                _playerMoney.SellMinions(data);
                _inventory.SellMinions(data);
                _inventory.Sell();
                gameObject.SetActive(false);
            }
        }
    }
}