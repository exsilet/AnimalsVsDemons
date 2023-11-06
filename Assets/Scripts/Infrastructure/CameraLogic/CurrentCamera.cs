using UnityEngine;

namespace Infrastructure.CameraLogic
{
    public class CurrentCamera : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";
        
        [SerializeField] protected AudioSource _areaMusic;

        public AudioSource AreaMusic => _areaMusic;
        
        private Camera _camera;

        private void Start()
        {
            if(!PlayerPrefs.HasKey(MusicVolume))
            {
                _areaMusic.volume = 1;
            }
            else
                _areaMusic.volume = PlayerPrefs.GetFloat(MusicVolume);

            _camera = Camera.main;
            _areaMusic.Play();
        }
    }
}