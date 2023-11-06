using System.Collections;
using UI.Element;
using UnityEngine;

namespace Enemy
{
    public class Soul : MonoBehaviour
    {
        protected const string SoulCounter = "SoulCounter";
        private Rigidbody2D _rigidbody;
        private Camera _camera;
        protected GameObject _target;
        protected int _reward;
        private BaseEnemy _enemy;

        public int Reward => _reward;

        public virtual void Start()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody2D>();
            _target = GameObject.FindGameObjectWithTag(SoulCounter);
            transform.SetParent(_camera.transform);
            StartCoroutine(Fly());
            GetReward();
        }        

        public void Init(BaseEnemy enemy)
            => _enemy = enemy;

        public virtual IEnumerator Fly()
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(MoveToTarget(transform, _target.transform.position));           
        }

        public IEnumerator MoveToTarget(Transform obj, Vector3 target)
        {
            Vector3 startPos = obj.position;
            float time = 0;
            while (time < 1)
            {
                obj.position = Vector3.Lerp(startPos, target, time * time);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ActorUI actionUI))
            {
                Destroy(gameObject);
            }
        }

        private void GetReward()
        {
            if (_enemy != null)
            {
                _reward = _enemy.Reward;
            }
        }
    }
}
