using UnityEngine;

namespace Infrastructure.Audio
{
    public class MinionDieSound : MonoBehaviour
    {
        private const string SoundVolume = "SoundVolume";

        public AudioSource[] _dieSounds;

        private void Start()
        {
            foreach (var sound in _dieSounds)
            {
                if (!PlayerPrefs.HasKey(SoundVolume)) sound.volume = 1;
            }
        }

        private void Update()
        {
            foreach (var sound in _dieSounds)
            {
                sound.volume = PlayerPrefs.GetFloat(SoundVolume);
            }
        }

        public AudioSource GetRandomSound()
        {
            int randomSound = Random.Range(0, _dieSounds.Length);
            return _dieSounds[randomSound];
        }
    }
}
