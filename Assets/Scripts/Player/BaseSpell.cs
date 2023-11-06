using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player
{
    public class BaseSpell : MonoBehaviour
    {
        private const string Explode = "Explode";
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private int _speed;
        [SerializeField] private int _damage;
        [SerializeField] private AudioSource _fly;
        [SerializeField] private AudioSource _explode;

        private List<BaseEnemy> _enemies;        
        private Animator _animator;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _fly.volume = 1;
                _explode.volume = 1;
            }
            else
            {
                _fly.volume = PlayerPrefs.GetFloat(SoundVolume);
                _explode.volume = PlayerPrefs.GetFloat(SoundVolume);
            }

            _enemies = new List<BaseEnemy>();
            _animator = GetComponent<Animator>();
            _fly.Play();
        }

        private void Update()
        {
            transform.position += Vector3.down  * _speed * Time.deltaTime;
        }        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.TryGetComponent(out BaseEnemy baseEnemy))
            {
                _enemies.Add(baseEnemy);               
            }

            if (collision.TryGetComponent(out Ground ground))
            {
                _speed = 0;
                _explode.Play();
                _animator.SetTrigger(Explode);

                foreach (var enemy in _enemies)
                {
                    if (enemy != null)
                    {
                        enemy.GetComponentInChildren<EnemyHealth>().TakeDamage(_damage);
                    }
                }
            }
        }               

        public void DestroyObject()
            => Destroy(gameObject);
    }
}
