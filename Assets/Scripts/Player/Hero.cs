using Data;
using Infrastructure.Service.SaveLoad;
using UnityEngine;

namespace Player
{
    public class Hero : MonoBehaviour, ISavedProgressReader
    {
        private int _gameLevel;

        public int GameLevel => _gameLevel;

        public void LoadProgress(PlayerProgress progress)
        {
            _gameLevel = progress.HeroState.GameLevel;
        }
    }
}
