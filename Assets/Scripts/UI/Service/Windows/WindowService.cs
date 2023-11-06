using UI.Service.Factory;
using UnityEngine;

namespace UI.Service.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void Open(WindowId windowID)
        {
            switch (windowID)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    EnableWindow(_uiFactory.Shop.gameObject);
                    break;
                case WindowId.Upgrade:
                    EnableWindow(_uiFactory.Upgrade.gameObject);
                    break;
                    
            }
        }

        private void EnableWindow(GameObject window)
        {
            window.SetActive(true);
        }
    }
}