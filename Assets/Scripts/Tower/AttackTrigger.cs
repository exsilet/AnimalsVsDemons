using System;
using Enemy;
using UnityEngine;

namespace Tower
{
    public class AttackTrigger : MonoBehaviour
    {
        public event Action<BaseEnemy> AttackZoneEntered;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                AttackZoneEntered?.Invoke(enemy);
            }
        }
    }
}

