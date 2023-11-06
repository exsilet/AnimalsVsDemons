using Data;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player
{
    public class CastSpell : MonoBehaviour, ISavedProgressReader
    {
        private const string SoundVolume = "SoundVolume";
        [SerializeField] private Button _cast;
        [SerializeField] private Transform _castPos;
        [SerializeField] private BaseSpell _spell;
        [SerializeField] private AudioSource _bookSound;

        private int _spellAmount;

        public int SpellAmount => _spellAmount;

        public event UnityAction<int> SpellUsed;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SoundVolume))
            {
                _bookSound.volume = 1;
            }
            else
                _bookSound.volume = PlayerPrefs.GetFloat(SoundVolume);
        }

        public void LoadProgress(PlayerProgress progress)
            => _spellAmount = progress.HeroState.SpellAmount;

        private void OnEnable() => 
            _cast.onClick.AddListener(Cast);

        private void OnDisable() => 
            _cast.onClick.RemoveListener(Cast);

        private void Cast()
        {
            if (_spellAmount != 0)
            {
                _bookSound.Play();
                Instantiate(_spell, _castPos.position, Quaternion.identity);
                _spellAmount--;
                SpellUsed?.Invoke(_spellAmount);
            }            
        }        
    }
}