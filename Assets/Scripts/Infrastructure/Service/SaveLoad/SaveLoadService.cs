using Data;
using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Service.SaveLoad
{
    public class SaveLoadService: ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        
        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {           
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();            
        }

        public void ResetProgress()
        {            
            PlayerPrefs.DeleteKey(ProgressKey);
            PlayerPrefs.Save();
            _gameFactory.ProgressWriters.Clear();
            _gameFactory.ProgressReaders.Clear();
            Debug.Log("ClearProgress");
        }
    }    
}