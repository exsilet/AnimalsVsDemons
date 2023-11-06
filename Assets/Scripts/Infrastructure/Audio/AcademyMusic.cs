using UnityEngine;

namespace Infrastructure.Audio
{
    public class AcademyMusic : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";

        [SerializeField] private AudioSource _audio;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MusicVolume))
            {
                _audio.volume = 1;
            }
            else
                _audio.volume = PlayerPrefs.GetFloat(MusicVolume);
            _audio.Play();
        }
    }
}
