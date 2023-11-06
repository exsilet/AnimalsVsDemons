using System.Collections.Generic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        GameObject CreatHudAcademy();
        GameObject CreateHudMenu();
        GameObject CreateDraggableItem();
        GameObject CreatTower(TowerTypeID typeId, Transform parent);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
    }
}