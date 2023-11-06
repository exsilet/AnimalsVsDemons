using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Element
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Button _sellButton;
        [SerializeField] private Image _iconOpen;

        public event UnityAction<LevelView> SellButtonClick;

        private void OnEnable()
        {
            _sellButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _sellButton.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            SellButtonClick?.Invoke(this);
        }

        public void BuyUpgrade()
        {
            ActiveLevel();
        }

        private void ActiveLevel()
        {
            _iconOpen.gameObject.SetActive(true);
            _sellButton.interactable = false;
        }
        
        public void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
        }
        
        public void ClosePanel(GameObject panel)
        {
            panel.SetActive(false);
        }
    }
}