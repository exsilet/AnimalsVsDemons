using System.Collections.Generic;
using Infrastructure;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.State;
using UnityEngine;

namespace UI
{
    public class StartLevelGame : MonoBehaviour
    {
        [SerializeField] private List<ButtonLevel> _levels;
        public LoadingCurtain Curtain;
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
        {
            foreach (ButtonLevel level in _levels)
            {
                level.StartLevelButtonClick += LevelOnStartLevelButtonClick;
            }
        }

        private void OnDisable()
        {
            foreach (ButtonLevel level in _levels)
            {
                level.StartLevelButtonClick -= LevelOnStartLevelButtonClick;
            }
        }

        private void LevelOnStartLevelButtonClick(ButtonLevel level, string levelName)
        {
            _stateMachine.Enter<LoadMenuState, string>(level.TransferTo);
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}