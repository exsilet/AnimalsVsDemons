using Data;
using Enemy;
using Infrastructure.CameraLogic;
using Infrastructure.Service.SaveLoad;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ActorUI : MonoBehaviour, ISavedProgress
    {
        private const string SpawnerController = "SpawnerController";

        [SerializeField] private HealthBarViewPlayer _hpBarPlayer;
        [SerializeField] private TMP_Text _soulCount;
        [SerializeField] private TMP_Text _countMonsters;
        [SerializeField] private Hero _hero;
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private CastSpell _spell;
        [SerializeField] private PlayerMoney _money;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _nextWave;
        [SerializeField] private Button _levelComplete;
        [SerializeField] private SpawnerController _spawnerController;
        [SerializeField] private RewardCalculation _reward;
        [SerializeField] private AudioSource _endGameMusic;

        private GameObject _spawner;
        private CurrentCamera _camera;
        private int _gameLevel;
        private int _difficult;

        public SpawnerController Spawner => _spawnerController;

        private void Awake()
        {
            _spawner = GameObject.FindGameObjectWithTag(SpawnerController);
            _spawnerController = _spawner.GetComponent<SpawnerController>();
            _levelComplete.gameObject.SetActive(false);
        }

        private void Start()
        {
            _spawnerController.GetEnemiesCount(_difficult);
            _soulCount.text = _money.CurrentSoul.ToString();
            _canvas.worldCamera = Camera.main;
            _reward.GetExtraSoul(_hero);
        }

        private void OnEnable()
        {
            _spawnerController.LevelCompleted += CompleteLevel;
            _spawnerController.LevelCompleted += _reward.GetReward;
            _spawnerController.WaveCompleted += ShowButton;
            _nextWave.onClick.AddListener(()=>_spawnerController.NextWave());
            _nextWave.onClick.AddListener(HideButton);
        }               

        private void OnDestroy()
        {
            _spawnerController.LevelCompleted -= _reward.GetReward;
            _spawnerController.WaveCompleted -= ShowButton;
            _nextWave.onClick.RemoveListener(()=>_spawnerController.NextWave());
            _nextWave.onClick.RemoveListener(HideButton);
            _spawnerController.LevelCompleted -= CompleteLevel;
            _heroHealth.HealthChanged -= UpdateHpBar;
            _money.CurrentSoulChanged -= UpdateSoulCount;
            _heroHealth.Died -= _reward.GetReward;
            _heroHealth.Died -= OnHeroDie;
        }

        public void Construct(Hero hero, HeroHealth heroHealth, CastSpell spell)
        {
            _hero = hero;
            _heroHealth = heroHealth;
            _spell = spell;
            _heroHealth.HealthChanged += UpdateHpBar;
            _money.CurrentSoulChanged += UpdateSoulCount;           
            _heroHealth.Died += _reward.GetReward;
            _heroHealth.Died += OnHeroDie;
        }

        public bool IsGameComplete()
        {
            if (_spawnerController.IsLevelComplete == true && _gameLevel == 3)
            {
                _camera.AreaMusic.Stop();
                _endGameMusic.Play();
                return true;
            }                
            return false;
        }

        public void UpdateProgress(PlayerProgress progress)
            => progress.HeroState.GameLevel = _gameLevel;

        public void LoadProgress(PlayerProgress progress)
        {
            _gameLevel = progress.HeroState.GameLevel;
            _difficult = progress.HeroState.Difficult;
        }

        public void ShowButton()
            => _nextWave.gameObject.SetActive(true);

        public void HideButton()
            => _nextWave.gameObject.SetActive(false);

        private void UpdateSoulCount(int soul)
            => _soulCount.text = soul.ToString();        

        private void UpdateHpBar() => 
            _hpBarPlayer.OnValueTextChanged(_heroHealth.Current);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Soul soul))
            {
                _money.AddMoney(soul.Reward);
            }
        }        

        private void OnHeroDie()
        {
            HideButton();
            _spawnerController.WaveCompleted -= ShowButton;
        }

        private void CompleteLevel()
        {
            _gameLevel++;
            HideButton();
            _spawnerController.WaveCompleted -= ShowButton;
        }                
    }
}