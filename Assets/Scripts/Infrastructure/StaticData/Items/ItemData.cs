using UnityEngine;

namespace Infrastructure.StaticData.Items
{
    public enum ItemType { Potion, Equipment}

    [CreateAssetMenu(fileName = "ItemData", menuName = "Item/NewItem")]
    public class ItemData : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public ItemType ItemType;

        public int AtkModifier;
        public float DfsModifier;
        public float Cooldown;
        public float HealthModifier;
        public float Price;

        //public virtual void Use(TowerStaticData data, float damage, float defence, float cooldown, float health) { }
    }
}
