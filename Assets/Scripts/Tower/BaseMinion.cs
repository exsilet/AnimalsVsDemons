using Enemy;
using Infrastructure.StaticData.Items;
using Infrastructure.StaticData.Tower;
using Items;
using UnityEditor;
using UnityEngine;
using DragAndDrop = Items.DragAndDrop;

namespace Tower
{
    public class BaseMinion : MonoBehaviour
    {
        protected const string Attack = "Attack";

        [SerializeField] protected BaseEnemy _enemy;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected int _damage;
        [SerializeField] protected float _defence;
        [SerializeField] protected TowerStaticData _towerData;
        [SerializeField] private ItemData _itemData;
        
        protected Animator _animator;
        protected bool _canAttack = true;
        private Sprite _itemIcon;

        public Sprite ItemIcon => _itemIcon;
        public float Defence => _defence;
        public int Damage => _damage;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _cooldown = _towerData.Cooldown;
            _damage = _towerData.Damage;
            _defence = _towerData.Defence;
        }

        public virtual void StartAttack() { }

        public void Init(BaseEnemy enemy)
            => _enemy = enemy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Treasure treasure))
            {
                if (_itemData == null)
                {
                    _itemData = treasure.ItemData;

                    if (treasure.ItemData.ItemType == ItemType.Equipment)
                    {
                        _itemIcon = treasure.ItemData.Icon;
                        _cooldown += treasure.ItemData.Cooldown;
                        _damage += treasure.ItemData.AtkModifier;
                        //treasure.TreasureSpawner.RemoveTreasures();
                        Destroy(treasure.gameObject);
                    }
                    else
                        treasure.GetComponentInChildren<DragAndDrop>().SetStartPosition();
                }
            }
        }        
                
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _enemy)) { StartAttack(); }
        }               
    }
}