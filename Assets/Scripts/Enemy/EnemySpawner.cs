using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Enemy;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] protected Transform[] _spawnPoints;
        [SerializeField] protected BaseEnemy _spawnedEnemy;
        [SerializeField] protected float _timeBetweenSpawn;
        [SerializeField] protected int _weakCount;
        [SerializeField] protected int _mediumCount;
        [SerializeField] protected int _strongCount;
        [SerializeField] protected int _bossCount;
        [SerializeField] protected SpawnerController _controller;
        [SerializeField] protected AudioSource _bossSound;
       
        private int _spawned;

        public List<EnemyTypeStaticData> _enemies;

        public int Weak => _weakCount;
        public int Medium => _mediumCount;
        public int Strong => _strongCount;
        public int Boss => _bossCount;

        public int Spawned => _spawned;

        protected virtual void Awake() { }        

        private void Start()
        {
            GetRandomPoint();
            Invoke(nameof(Spawn), 2f);
        }

        public void SetDifficult(int difficultIndicator)
        {
            if (_weakCount > 0)
                _weakCount += difficultIndicator;

            if (_mediumCount > 0)
                _mediumCount += difficultIndicator;

            if (_strongCount > 0)
                _strongCount += difficultIndicator;

            GetSpawnedEnemies();
        }

        private void GetSpawnedEnemies()
            => _spawned = _weakCount + _mediumCount + _strongCount + _bossCount;

        private void Spawn(Transform parent)
            => StartCoroutine(SpawnEnemies(_weakCount, _mediumCount, _strongCount, _bossCount, parent));

        private IEnumerator SpawnEnemies(int weakCount, int mediumCount, int strongCount, int bossCount, Transform parent)
        {
            var delay = new WaitForSeconds(_timeBetweenSpawn);

            for (int i = 0; i < weakCount; i++)
            {
                yield return CreateMonster(delay, EnemyTypeID.Weak, parent);
            }
            for (int i = 0; i < mediumCount; i++)
            {
                yield return CreateMonster(delay, EnemyTypeID.Medium, parent);
            }
            for (int i = 0; i < strongCount; i++)
            {
                yield return CreateMonster(delay, EnemyTypeID.Strong, parent);
            }
            for (int i = 0; i < bossCount; i++)
            {
                yield return CreateMonster(delay, EnemyTypeID.Boss, parent);
            }
        }

        private object CreateMonster(WaitForSeconds delay, EnemyTypeID typeID, Transform parent)
        {
            var randomEnemy = GetRandomEnemy<BaseEnemy>(typeID);
            _spawnedEnemy = Instantiate(randomEnemy, parent);
            _spawnedEnemy.GetComponentInChildren<EnemyHealth>().Died += _controller.OnEnemyDied;
            
            if(typeID == EnemyTypeID.Boss)
                _bossSound.Play();
            
            return delay;
        }

        private T GetRandomEnemy<T>(EnemyTypeID type) where T : BaseEnemy
        {
            return (T)_enemies.Where(e => e.EnemyType == type).OrderBy(o => Random.value).First().GetRandomPrefab();
        }

        private Transform GetRandomPoint()
        {
            int randomPoint = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomPoint];
        }        
    }
}

