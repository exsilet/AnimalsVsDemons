using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class SpawnerController : MonoBehaviour
    {
        [SerializeField] private EnemySpawner[] _spawners;

        private int _enemiesCount;
        private float _enemiesPercent = 100;
        private float _killedEnemiesPercent;
        private int _spawned;
        private int _killedEnemies;
        private int _currentSpawnerIndex;
        private int _wavesCount;
        private EnemySpawner _currentSpawner;
        private bool _canChange = false;
        private bool _isLevelComplete;

        public bool IsLevelComplete => _isLevelComplete;

        public event UnityAction WaveCompleted;
        public event UnityAction LevelCompleted;
        public event UnityAction WaveStarted;

        public EnemySpawner CurrentSpawner => _currentSpawner;
        public float KilledEnemies => _killedEnemiesPercent;        

        private void Start()
        {
            _wavesCount = _spawners.Length;
        }

        private void OnEnable()
        {
            WaveCompleted += ShowBackground;
            WaveStarted += HideBackground;
        }

        private void OnDestroy()
        {
            WaveCompleted -= ShowBackground;
            WaveStarted -= HideBackground;
        }        

        public void GetEnemiesCount(int difficult)
        {
            foreach (var spawner in _spawners)
            {
                spawner.SetDifficult(difficult);
                _enemiesCount += spawner.Weak + spawner.Medium + spawner.Strong + spawner.Boss;
            }
        }

        public void OnEnemyDied(BaseEnemy enemy)
        {
            _spawned--;
            _killedEnemies++;

            if (_spawned == 0)
            {
                _wavesCount--;
                CheckLastWave();
                WaveCompleted?.Invoke();
            }

            enemy.GetComponentInChildren<EnemyHealth>().Died -= OnEnemyDied;
        }

        public void CalculatePercentage()
            => _killedEnemiesPercent = (_enemiesPercent / _enemiesCount) * _killedEnemies;

        public void NextWave()
        {
            WaveStarted?.Invoke();

            if (_currentSpawnerIndex != _spawners.Length)
            {
                _spawners[_currentSpawnerIndex].gameObject.SetActive(true);

                if (_canChange == true)
                {
                    SetSpawner(++_currentSpawnerIndex);
                    _spawners[_currentSpawnerIndex].gameObject.SetActive(true);
                }
                else
                    SetSpawner(_currentSpawnerIndex);
                _canChange = true;
            }
        }

        private void SetSpawner(int index)
        {
            _currentSpawner = _spawners[index];
            _spawned = _currentSpawner.Spawned;
        }

        private void HideBackground()
        {
        }

        private void ShowBackground()
        {
        }

        private void CheckLastWave()
        {
            if (_wavesCount == 0 && _spawned == 0)
            {
                _isLevelComplete = true;
                LevelCompleted?.Invoke();
            }
        }
    }
}