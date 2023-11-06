using System;
using UI.Forms;
using UI.Service.Windows;

namespace Infrastructure.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public BaseWindow Prefab;
    }
}