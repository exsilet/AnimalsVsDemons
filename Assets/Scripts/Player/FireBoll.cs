using Enemy;
using Tower;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class FireBoll : MonoBehaviour
    {
        [SerializeField]private float _speed;
        [SerializeField] private int _damage;

        private BaseEnemy _target;
        private Rigidbody2D _rigidbody;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();

        private void Update() => 
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position,
                _speed * Time.deltaTime);

        public void Init(BaseEnemy target) => 
            _target = target;

        private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.GetComponent<IHealth>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}