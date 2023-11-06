using UnityEngine;
using UnityEngine.UI;

namespace UI.Forms
{
    public abstract class BaseWindow : MonoBehaviour
    {
        public Button CloseButton;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(ClosePanel);
        }

        private void ClosePanel()
        {
            if (gameObject.GetComponent<ShopWindow>() != null)
            {
                gameObject.GetComponent<ShopWindow>().Inactive();
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }
}