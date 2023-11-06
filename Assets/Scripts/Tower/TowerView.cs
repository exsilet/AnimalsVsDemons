using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using TMPro;
using UI.Element;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tower
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _background;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private EventTrigger _event;

        private TowerStaticData _item;
        private OpenPanelMinions _panelMinions;
    
        public event UnityAction<TowerStaticData, TowerView> SellButtonClick;
        public event UnityAction<TowerStaticData> Highlighted;
        
    
        private void OnEnable()
        {
            _sellButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _sellButton.onClick.RemoveListener(OnButtonClick);
        }
    
        public void Initialize(TowerStaticData item)
        {
            _item = item;
            _iconImage.sprite = item.UIIcon;
            _price.text = item.Price.ToString();
        }

        public void OnMouseDetected()
        {
            Highlighted?.Invoke(_item);
        }

        public void CloseMinions()
        {
            _sellButton.interactable = false;
            _iconImage.color = new Color(1f, 1f, 1f, 0.2f);
            _event.enabled = false;
        }

        public void OpenMinions()
        {
            _sellButton.interactable = true;
            _iconImage.color = new Color(1f, 1f, 1f, 1f);
            _event.enabled = true;
        }

        private void OnButtonClick()
        {
            SellButtonClick?.Invoke(_item, this);
        }
    }
}