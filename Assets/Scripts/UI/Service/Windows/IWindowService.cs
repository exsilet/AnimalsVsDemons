using Infrastructure.Service;

namespace UI.Service.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowID);
    }
}