using System.Collections;
using Infrastructure.Audio;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyDieSound _audioController;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Soul _soulPrefab;

        public GameObject DeathFx;

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0) Die();
        }

        private void Die()
        {
            AudioSource dieSound;
            _health.HealthChanged -= OnHealthChanged;
            dieSound = _audioController.GetRandomSound();
            dieSound.Play();
            GameObject fx = Instantiate(DeathFx, transform.position, Quaternion.identity);
            var soul = Instantiate(_soulPrefab, transform.position, Quaternion.identity);
            soul.Init(GetComponentInChildren<BaseEnemy>());
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }        
    }
}
