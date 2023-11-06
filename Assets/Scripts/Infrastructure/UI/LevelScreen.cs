using System.Collections.Generic;
using Data;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.UI
{
    public class LevelScreen : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Button _portArea;
        [SerializeField] private Button _marketArea;
        [SerializeField] private Button _mageArea;
        [SerializeField] private Button _academy;        
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private List<Image> _fogImages;

        private int _gameLevel;

        public event UnityAction PortLoaded;
        public event UnityAction MarketLoaded;
        public event UnityAction MageLoaded;
        public event UnityAction AcademyLoaded;

        private void Start()
        {
            _marketArea.interactable = false;
            _mageArea.interactable = false;
            OpenLevels();
        }                

        private void OnEnable()
        {
            _portArea.onClick.AddListener(LoadPort);
            _marketArea.onClick.AddListener(LoadMarket);
            _mageArea.onClick.AddListener(LoadMage);
            _academy.onClick.AddListener(LoadAcademy);
        }

        private void OnDisable()
        {
            _portArea.onClick.RemoveListener(LoadPort);
            _marketArea.onClick.RemoveListener(LoadMarket);
            _mageArea.onClick.RemoveListener(LoadMage);
            _academy.onClick.RemoveListener(LoadAcademy);
        }

        public void UpdateProgress(PlayerProgress progress)
            => progress.HeroState.GameLevel = _gameLevel;

        public void LoadProgress(PlayerProgress progress)
            => _gameLevel = progress.HeroState.GameLevel;

        private void LoadAcademy()
            => AcademyLoaded?.Invoke();

        private void LoadMage()
            => MageLoaded?.Invoke();

        private void LoadMarket()
            => MarketLoaded?.Invoke();

        private void LoadPort()
            => PortLoaded?.Invoke();

        private void OpenLevels()
        {
            if (_gameLevel > 0)
            {
                for (int i = 0; i < _gameLevel; i++)
                {
                    _buttons[i].interactable = false;
                    _fogImages[i].gameObject.SetActive(false);
                    _buttons[_gameLevel].interactable = true;
                }
            }                                    
        }        
    }    
}
