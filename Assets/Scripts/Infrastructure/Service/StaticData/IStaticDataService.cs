using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;
using Infrastructure.StaticData.Windows;
using UI.Service.Windows;

namespace Infrastructure.Service.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        TowerStaticData ForTower(TowerTypeID typeID);
        WindowConfig ForWindow(WindowId windowID);  
    }
}