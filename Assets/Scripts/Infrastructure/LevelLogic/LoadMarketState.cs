using Infrastructure.State;

namespace Infrastructure.LevelLogic
{
    public class LoadMarketState : IState
    {
        private const string MarketArea = "MarketArea";
        private GameStateMachine gameStateMachine;
        private SceneLoader _sceneLoader;

        public LoadMarketState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this._sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(MarketArea);
        }

        public void Exit()
        {
        }
    }
}