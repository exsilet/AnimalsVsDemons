using System.Collections.Generic;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using Tower;
using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<TowerSpawner> _spawners;
        [SerializeField] private List<TowerStaticData> _minions;
        
        private TowerStaticData _towerStaticData;
        private TowerSpawner _position;

        private void OnEnable()
        {
            foreach (TowerSpawner spawner in _spawners)
            {
                spawner.CreateMinion += SpawnerOnCreateMinion;                
            }
        }

        private void OnDisable()
        {
            foreach (TowerSpawner spawner in _spawners)
            {
                spawner.CreateMinion -= SpawnerOnCreateMinion;
            }
        }

        public void BuyMinions(TowerStaticData data)
        {
            _minions.Add(data);
            _towerStaticData = data;
        }

        public void SellMinions(TowerStaticData data)
        {
            _towerStaticData = data;
            _minions.Remove(_towerStaticData);
        }

        public void CurrentData(TowerStaticData data)
            => _towerStaticData = data;

        public void Sell()
        {
            _position.DestroyMinions();
            _position.IsCreateTower();
        }

        public void SpawnMinions()
        {
            _position.BuyTowerSpawn(_towerStaticData);
        }            

        public void SetSpawnPosition(TowerSpawner position)
            => _position = position;

        private void SpawnerOnCreateMinion(TowerSpawner position)
            => _position = position;
    }
}