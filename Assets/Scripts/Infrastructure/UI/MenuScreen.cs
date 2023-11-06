using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.UI
{
    public class MenuScreen : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";

        [SerializeField] private GameObject _menuScreen;
        [SerializeField] private GameObject _settingsScreen;
        [SerializeField] private GameObject _levelScreen;
        [SerializeField] private GameObject _tutorialScreen;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _howToPlay;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _mapToMenu;
        [SerializeField] private Button _settingsToMenu;
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private AudioSource _audio;

        private string _disactivate = "Disactivate";

        public event UnityAction GameStarted;
        public event UnityAction RulesShowed;

        private ISaveLoadService _saveLoadService;

        private void Awake()
            => _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MusicVolume))
            {
                _audio.volume = 1;
            }
            else
                _audio.volume = PlayerPrefs.GetFloat(MusicVolume);

            _audio.Play();
            
            _menuScreen.SetActive(true);
            _levelScreen.SetActive(false);
            _settingsScreen.SetActive(false);
            _tutorialScreen.SetActive(false);
            CheckDisactivateKey();
        }

        private void Update()
            => _audio.volume = PlayerPrefs.GetFloat(MusicVolume);

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButton);
            _howToPlay.onClick.AddListener(OnTutorialButton);
            _settings.onClick.AddListener(OnSettingsButton);
            _mapToMenu.onClick.AddListener(ReturnFromMap);
            _settingsToMenu.onClick.AddListener(ReturneFromSettings);
            _gameRules.RulesShowed += OnRulesShowed;
        }        

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButton);
            _howToPlay.onClick.RemoveListener(OnTutorialButton);
            _mapToMenu.onClick.RemoveListener(ReturnFromMap);
            _settingsToMenu.onClick.RemoveListener(ReturneFromSettings);
            _gameRules.RulesShowed -= OnRulesShowed;
        }
        
        private void OnRulesShowed() 
            => _menuScreen.SetActive(true);

        private void OnStartButton()
            => Invoke(nameof(OpenMap), 0.6f);

        private void OnTutorialButton()
            => Invoke(nameof(OpenTutorial), 0.6f);

        private void OnSettingsButton()
            => Invoke(nameof(OpenSettings), 0.6f);       

        private void OpenMap()
        {
            _menuScreen.SetActive(false);
            _levelScreen.SetActive(true);
            GameStarted?.Invoke();
            PlayerPrefs.SetString("Disactivate", _disactivate);
            PlayerPrefs.Save();
        }

        private void OpenTutorial()
        {
            _menuScreen.SetActive(false);
            _tutorialScreen.SetActive(true);
            RulesShowed?.Invoke();
        }

        private void ReturneFromSettings()
        {
            _saveLoadService.SaveProgress();
            _menuScreen.SetActive(true);
            _settingsScreen.SetActive(false);
        }

        private void ReturnFromMap()
        {
            _menuScreen.SetActive(true);
            _levelScreen.SetActive(false);
        }
            
        private void OpenSettings()
            => _settingsScreen.SetActive(true);

        private void CheckDisactivateKey()
        {
            if (PlayerPrefs.HasKey("Disactivate"))
            {
                OpenMap();
            }
        }

        private void ClearDisactivateKey()
            => PlayerPrefs.DeleteKey(_disactivate);
    }
}
