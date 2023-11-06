using UnityEngine;

namespace Tower.Dragon
{
    public class SecondLvlFlame : Missile
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _flameSound;

        private const string SecondFlame = "SecondFlame";
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
            _animator.Play(SecondFlame);
            _flameSound.Play();
        }
    }
}
