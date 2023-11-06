using Enemy;
using UnityEngine;

namespace Infrastructure.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyType")]
    public class EnemyTypeStaticData : ScriptableObject
    {
        [SerializeField] private BaseEnemy _enemyPrefab;
        [SerializeField] private BaseEnemy[] _prefabs;
        [SerializeField] private EnemyTypeID _enemyType;

        public EnemyTypeID EnemyType => _enemyType;

        public BaseEnemy GetRandomPrefab()
        {
            int randomPrefab = Random.Range(0, _prefabs.Length);
            _enemyPrefab = _prefabs[randomPrefab];
            return _enemyPrefab;
        }
    }
}

