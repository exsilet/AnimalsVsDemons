using System;
using Data;
using Infrastructure.Service;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Tower;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedProgressReader
    {
        private State _state;

        public event UnityAction HealthChanged;
        public event UnityAction Died;

        private void Start()
        {
            HealthChanged?.Invoke();
        }

        private void Awake()
        {
            IPersistentProgressService progress = AllServices.Container.Single<IPersistentProgressService>();
        }

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                _state.CurrentHP = (int)value;
                HealthChanged?.Invoke();
            }
        }

        public float Max { get => _state.MaxHP; set => _state.MaxHP = (int)value; }

        public void TakeDamage(int damage)
        {
            Current -= damage;

            if (Current <= 0)
                Die();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            _state.ResetHP();
        }

        public void RemoveCat()
            => Destroy(gameObject);

        private void Die()
        {
            Died?.Invoke();         
        }
    }
}