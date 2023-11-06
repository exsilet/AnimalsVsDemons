using System.Collections;
using Player;
using TMPro;
using UI.Element;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class RewardCalculation : MonoBehaviour
    {
        [SerializeField] private Image _rewardWindow;
        [SerializeField] private Image _oneStar;
        [SerializeField] private Image _twoStars;
        [SerializeField] private Image _threeStars;
        [SerializeField] private Image _endGameImage;
        [SerializeField] private TMP_Text _rewardAmount;
        [SerializeField] private TMP_Text _killedPercent;
        [SerializeField] private PrizeSoul _prizeSoulPrefab;        
        [SerializeField] private ActorUI _actorUI;
        [SerializeField] private GameObject _advMoney;
        [SerializeField] private Button _soulsForAdd;
        [SerializeField] private int _ADVSoul;

        private int _lowPercent = 25; private int _mediumPercent = 50; private int _highPercent = 75;
        private int _lowReward = 30; private int _mediumReward = 50; private int _highReward = 100;
        private int _lowSoulsAmount = 3; private int _mediumSoulsAmount = 5; private int _highSoulsAmount = 10;

        // private void OnEnable()
        //     => _soulsForAdd.onClick.AddListener(ShowRewardAdd);
        // private void OnDisable()
        //     => _soulsForAdd.onClick.RemoveListener(ShowRewardAdd);

        public void GetExtraSoul(Hero hero)
        {
            if (hero.GameLevel > 1)
            {
                StartCoroutine(SpawnSoul(hero.GameLevel + 1));
            }
        }

        // public void ShowRewardAdd()
        //     => VideoAd.Show(OnAddOpen, OnAddClosed);

        public void GetReward()
        {           
            _actorUI.Spawner.CalculatePercentage();
            _rewardWindow.gameObject.SetActive(true);
            _advMoney.SetActive(true);

            if (_actorUI.IsGameComplete())
            {
                _rewardWindow.gameObject.SetActive(false);
                _endGameImage.gameObject.SetActive(true);
            }
            else if (_actorUI.Spawner.KilledEnemies <= _lowPercent)
            {
                SetRewardValues(_lowReward, _oneStar);
                _killedPercent.text = _actorUI.Spawner.KilledEnemies.ToString();
                StartCoroutine(SpawnSoul(_lowSoulsAmount));
            }
            else if (_actorUI.Spawner.KilledEnemies <= _mediumPercent)
            {
                SetRewardValues(_mediumReward, _twoStars);
                _killedPercent.text = _actorUI.Spawner.KilledEnemies.ToString();
                StartCoroutine(SpawnSoul(_mediumSoulsAmount));
            }
            else if (_actorUI.Spawner.KilledEnemies > _highPercent)
            {
                SetRewardValues(_highReward, _threeStars);
                _killedPercent.text = _actorUI.Spawner.KilledEnemies.ToString();
                StartCoroutine(SpawnSoul(_highSoulsAmount));
            }
        }

        private IEnumerator SpawnSoul(int amount)
        {
            for (int i = amount; i > 0; i--)
            {
                Instantiate(_prizeSoulPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
            }
        }                        

        private void SetRewardValues(int reward, Image rewardImage)
        {
            rewardImage.gameObject.SetActive(true);            
            _rewardAmount.text = reward.ToString();
        }

        private void OnAddOpen()
        {
            AudioListener.volume = 0;
        }

        private void OnAddClosed()
        {
            AudioListener.volume = 1;
            StartCoroutine(SpawnSoul(_ADVSoul));
            _soulsForAdd.gameObject.SetActive(false);
        }        
    }
}
