using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ButtonLevel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _transferTo;

        public string TransferTo => _transferTo;
        
        public event UnityAction<ButtonLevel, string> StartLevelButtonClick;

        private void OnEnable()
        {
            _button.onClick.AddListener(StartLevelGame);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(StartLevelGame);
        }

        private void StartLevelGame()
        {
            StartLevelButtonClick?.Invoke(this, _transferTo);
        }
    }
}