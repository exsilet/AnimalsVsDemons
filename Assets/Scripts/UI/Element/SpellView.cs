using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Element
{
    public class SpellView : MonoBehaviour
    {
        [SerializeField] private Button _sellButton;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _iconOpen;

        public event UnityAction<SpellView> SellButtonClick;

        public Button SellButton => _sellButton;

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
            _icon.gameObject.SetActive(false);
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