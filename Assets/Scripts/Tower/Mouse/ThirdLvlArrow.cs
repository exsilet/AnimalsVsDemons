using UnityEngine;

namespace Tower.Mouse
{
    public class ThirdLvlArrow : Missile
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _arrowSound;

        private const string ThirdArrow = "ThirdArrow";
        private Animator _animator;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _arrowSound.volume = 1;
            }
            else
                _arrowSound.volume = PlayerPrefs.GetFloat(SoundVolume);

            _arrowSound.Play();
            _animator = GetComponent<Animator>();
            _animator.Play(ThirdArrow);
            _arrowSound.Play();
        }        
    }
}
