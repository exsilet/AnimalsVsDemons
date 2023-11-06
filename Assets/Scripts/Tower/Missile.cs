using Enemy;
using Infrastructure.StaticData.Tower;
using UnityEngine;

namespace Tower
{
    public class Missile : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected int _damage;
        [SerializeField] private BaseEnemy _target;
        [SerializeField] private TowerStaticData _towerData;
        [SerializeField] private BaseMinion _baseMinion;

        private void Awake()
            => _speed = _towerData.MissileSpeed;

        private void Update()
        {
            if (_target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            }
            else
                Destroy(gameObject);

            if (_baseMinion != null) _damage = _baseMinion.Damage;
        }

        public void Init(BaseEnemy target)
            => _target = target;

        public void InitMinion(BaseMinion minion)
            => _baseMinion = minion;

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.GetComponentInChildren<EnemyHealth>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
