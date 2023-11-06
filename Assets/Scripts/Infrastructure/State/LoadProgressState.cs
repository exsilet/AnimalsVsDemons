using Data;
using Infrastructure.LevelLogic;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;

namespace Infrastructure.State
{
    public class LoadProgressState: IState
    {
        private const string MenuScene = "MenuScene";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadMenuState, string>(MenuScene);
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew()
        {            
            _progressService.Progress = 
                _saveLoadService.LoadProgress() 
                ?? NewProgress();
        }
        
        private PlayerProgress NewProgress()
        {            
            var progress =  new PlayerProgress(initialLevel: MenuScene);
            progress.HeroState.ResetHP();

            return progress;
        }
    }
}