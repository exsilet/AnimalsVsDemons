using UnityEngine;

namespace Infrastructure.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public float Health;
        public int Damage;
        public float Cooldown;
        public float Speed;
        public int Reward;
    }
}

