using UnityEngine;

namespace Tower.Dragon
{
    public class FirstLevelFlame : Missile
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _flameSound;

        private const string FirstFlame = "FirstFlame";
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
            _animator.Play(FirstFlame);
            _flameSound.Play();
        }
    }
}
