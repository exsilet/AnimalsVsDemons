using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Element
{
    public class MinionView : MonoBehaviour
    {
        [SerializeField] private Button _sellButton;
        [SerializeField] private Image _iconOpenImage;
        [SerializeField] private Image _iconColorImage;
        [SerializeField] private TowerStaticData _data;

        public event UnityAction<MinionView> SellButtonClick;

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
            _iconOpenImage.enabled = false;
            _iconColorImage.color = new Color(1f,1f,1f,1f);
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
