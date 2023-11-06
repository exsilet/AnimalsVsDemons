using UnityEngine;

namespace Infrastructure.StaticData.Tower
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "StaticData/Tower")]
    public class TowerStaticData : ScriptableObject
    {
        public string NameRus;
        public string NameEng;
        [Range(1, 3)]public int Level = 1;
        [Range(1f, 30f)] public float CurrentHP;
        [Range(1f, 30f)] public float MaxHP;
        [Range(1, 50)] public int Damage;
        [Range(1f, 10f)] public float Defence;
        [Range(1f, 3f)] public float Cooldown;
        [Range(1f, 5f)] public float MissileSpeed;

        public Sprite UIIcon;
        public int Price;
        public GameObject Prefab;
        public TowerTypeID TowerTypeID;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}