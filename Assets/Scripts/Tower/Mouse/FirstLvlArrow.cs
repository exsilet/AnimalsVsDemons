using UnityEngine;

namespace Tower.Mouse
{
    public class FirstLvlArrow : Missile
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _arrowSound;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _arrowSound.volume = 1;
            }
            else
                _arrowSound.volume = PlayerPrefs.GetFloat(SoundVolume);

            _arrowSound.Play();
        }
    }
}
