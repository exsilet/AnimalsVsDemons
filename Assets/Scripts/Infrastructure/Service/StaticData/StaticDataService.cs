using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using Infrastructure.StaticData.Windows;
using UI.Service.Windows;
using UnityEngine;

namespace Infrastructure.Service.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataTowerPath = "StaticData/Tower";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";
        private Dictionary<TowerTypeID, TowerStaticData> _tower;
        private Dictionary<WindowId,WindowConfig> _windowConfigs;

        public void Load()
        {
            _tower = Resources
                .LoadAll<TowerStaticData>(StaticDataTowerPath)
                .ToDictionary(x => x.TowerTypeID, x => x);
         
            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public TowerStaticData ForTower(TowerTypeID typeID) =>
            _tower.TryGetValue(typeID, out TowerStaticData staticData) 
                ? staticData 
                : null;

        public WindowConfig ForWindow(WindowId windowID) =>
            _windowConfigs.TryGetValue(windowID, out WindowConfig windowConfig) 
                ? windowConfig 
                : null;
    }
}