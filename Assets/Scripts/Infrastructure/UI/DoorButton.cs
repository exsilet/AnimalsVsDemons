using UnityEngine;

namespace Infrastructure.UI
{
    public class DoorButton : MonoBehaviour
    {
        private const string SoundVolume = "SoundVolume";

        [SerializeField] private AudioSource _open;
        [SerializeField] private AudioSource _close;

        private void Update()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _open.volume = 1;
                _close.volume = 1;
            }
            else
            {
                _open.volume = PlayerPrefs.GetFloat(SoundVolume);
                _close.volume = PlayerPrefs.GetFloat(SoundVolume);
            }
        }

        public void OpenDoor()
            => _open.Play();

        public void CloseDoor()
            => _close.Play();       
    }
}
