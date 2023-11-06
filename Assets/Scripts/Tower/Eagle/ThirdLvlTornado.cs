using Enemy;
using UnityEngine;

namespace Tower.Eagle
{
    public class ThirdLvlTornado : Missile
    {
        [SerializeField] private AudioSource _tornadoSound;

        private const string SoundVolume = "SoundVolume";

        private float _speedReduce = 0.3f;
        private const string ThirdTornado = "ThirdTornado";
        private Animator _animator;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _tornadoSound.volume = 1;
            }
            else
                _tornadoSound.volume = PlayerPrefs.GetFloat(SoundVolume);

            _animator = GetComponent<Animator>();
            _animator.Play(ThirdTornado);
            _tornadoSound.Play();
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.OnTornadoEnter(_speedReduce);
                enemy.GetComponentInChildren<EnemyHealth>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
