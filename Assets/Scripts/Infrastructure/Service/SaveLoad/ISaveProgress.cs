using Data;

namespace Infrastructure.Service.SaveLoad
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}