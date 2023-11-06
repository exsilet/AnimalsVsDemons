using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Service.SaveLoad;
using UI.Forms;
using UnityEngine;

namespace UI.Service.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IAssetProvider _assets;
        private Transform _uiRoot;
        private ShopWindow _shop;
        private UpgradeMinions _upgrade;

        public UIFactory(IAssetProvider assets) => _assets = assets;
        public ShopWindow Shop => _shop;
        public UpgradeMinions Upgrade => _upgrade;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public GameObject CreateUIRoot()
        {
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
            _shop = GetShop();
            _upgrade = GetUpgrade();

            return _uiRoot.gameObject;
        }

        private ShopWindow GetShop()
        {
            ShopWindow shop = _uiRoot.gameObject.GetComponentInChildren<ShopWindow>();
            ProgressReaders.Add(shop);
            shop.gameObject.SetActive(false);
            return shop;
        }
        
        private UpgradeMinions GetUpgrade()
        {
            UpgradeMinions upgrade = _uiRoot.gameObject.GetComponentInChildren<UpgradeMinions>();
            upgrade.gameObject.SetActive(false);
            return upgrade;
        }
    }
}