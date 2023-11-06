using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Infrastructure.UI
{
    class GameRules : MonoBehaviour
    {
        private const string NextAnim = "NextAnim";

        [SerializeField] private GameObject _gameRules;
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private Button _next;
        [SerializeField] private List<Image> _images;

        private Animator _animator;
        private int _animNumber;
        private int _animCount;

        public event UnityAction RulesShowed;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animCount = _images.Count;
        }

        private void OnEnable()
        {
            _menuScreen.RulesShowed += OpenRulesPanel;
            _next.onClick.AddListener(ShowRules);
        }

        private void OnDisable()
        {
            //_menuScreen.ShowRules -= OpenRulesPanel;
            _next.onClick.RemoveListener(ShowRules);
        }

        private void OpenRulesPanel() 
            => _gameRules.SetActive(true);

        private void ShowRules()
        {
            _animNumber++;

            foreach (var image in _images)
            {
                if (image.gameObject.activeSelf == false)
                {
                    image.gameObject.SetActive(true);                    
                    break;
                }
            }            

            if (_animNumber == _animCount)
            {                
                _animNumber = 0;

                for (int i = _images.Count-1; i >= 1; i--)
                {
                    print(i);
                    _images[i].gameObject.SetActive(false);
                    
                }

                _gameRules.SetActive(false);
                RulesShowed?.Invoke();
            }
        }
    }
}
