using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Player;
using Tower;
using UI.Element;
using UI.Forms;
using UI.Service.Factory;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;
        private IState _stateImplementation;
        private Camera _camera;
        
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IUIFactory uiFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            GameObject hud = _gameFactory.CreateHud();
            InitHud(hero, hud);
            GameObject additionalTool = _gameFactory.CreateDraggableItem();            
            InitUiRoot(hero, hud, additionalTool);

            foreach (var towerSpawner in hero.GetComponentsInChildren<TowerSpawner>())
            {
                towerSpawner.Construct(_uiFactory);
            }
        }

        private void InitUiRoot(GameObject hero, GameObject hud, GameObject additionalTool)
        {
            GameObject uiRoot = _uiFactory.CreateUIRoot();
            
            uiRoot.GetComponentInChildren<ShopWindow>(true).Construct(
                hud.GetComponent<PlayerMoney>(),
                hero.GetComponent<Inventory>());

            uiRoot.GetComponentInChildren<UpgradeMinions>(true).Construct(
                hud.GetComponent<PlayerMoney>(),
                hero.GetComponent<Inventory>());
            
            additionalTool.GetComponent<DraggableItem>().Construct(uiRoot.GetComponentInChildren<UpgradeMinions>(true), hero.GetComponent<Inventory>());
        }

        private void InitHud(GameObject hero, GameObject hud)
        {
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<Hero>(),hero.GetComponent<HeroHealth>(), hero.GetComponent<CastSpell>());
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);

            foreach (ISavedProgressReader progressReader in _uiFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
    }
}
