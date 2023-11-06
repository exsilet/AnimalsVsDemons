using UnityEngine;

namespace Tower.Dragon
{
    public class ThirdLvlFlame : Missile
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _flameSound;

        private const string ThirdFlame = "ThirdFlame";
        private Animator _animator;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _flameSound.volume = 1;
            }
            else
                _flameSound.volume = PlayerPrefs.GetFloat(SoundVolume);

            _animator = GetComponent<Animator>();
            _animator.Play(ThirdFlame);
            _flameSound.Play();
        }
    }
}
