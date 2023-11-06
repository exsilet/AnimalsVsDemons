using System.Collections.Generic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using UI.Forms;
using UnityEngine;

namespace UI.Service.Factory
{
    public interface IUIFactory : IService 
    { 
        GameObject CreateUIRoot();
        ShopWindow Shop { get; }
        UpgradeMinions Upgrade { get; }
        public List<ISavedProgressReader> ProgressReaders { get; }
    }
}