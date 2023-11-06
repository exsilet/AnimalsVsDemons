using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using TMPro;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class OpenPanelMinions : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private Image _imageIcon;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _cooldownText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private Image _itemIcon;        
    
        private TowerStaticData _data;        

        public void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void ClosePanel(GameObject panel)
        {
            panel.SetActive(false);
        }

        public void Show(TowerStaticData data)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                _textTitle.text = data.NameRus;
            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {
                _textTitle.text = data.NameEng;
            }

            _imageIcon.sprite = data.UIIcon;
            _damageText.text = data.Damage.ToString();
            _cooldownText.text = data.Cooldown.ToString();
            _healthText.text = data.MaxHP.ToString();                                               
        }

        public void SetItemIcon(BaseMinion minion)
        {
            _itemIcon.sprite = minion.ItemIcon;
            _itemIcon.color = new Color(1, 1, 1, 1);

            if (minion.ItemIcon == null)
            {
                _itemIcon.color = Color.clear;
            }
        }
    }
}