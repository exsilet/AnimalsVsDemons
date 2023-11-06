using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Audio
{
    public class SoundSlider : MonoBehaviour
    {
        private const string SoundVolume = "SoundVolume";
        [SerializeField] private Slider _slider;

        private float _volume;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _slider.value = 1;
            }
            else
                _slider.value = PlayerPrefs.GetFloat(SoundVolume);

        }

        private void Update()
        {
            if (_volume != _slider.value)
            {
                PlayerPrefs.SetFloat(SoundVolume, _slider.value);
                PlayerPrefs.Save();
                _volume = _slider.value;
            }
        }
    }
}
