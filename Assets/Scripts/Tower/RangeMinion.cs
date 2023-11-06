using System.Collections;
using UnityEngine;

namespace Tower
{
    public class RangeMinion : BaseMinion
    {
        [SerializeField] private Transform _firePos;
        [SerializeField] private Missile _missile;

        public override void StartAttack()
            => StartCoroutine(Shoot());
        
        private IEnumerator Shoot()
        {
            if (_canAttack)
            {
                _canAttack = false;
                _animator.SetTrigger(Attack);
                Invoke(nameof(SetArrow), 0.3f);
                yield return new WaitForSeconds(_cooldown);
                _canAttack = true;
            }
        }

        private void SetArrow()
        {
            Missile missile = Instantiate(_missile, _firePos.position, Quaternion.identity);
            missile.Init(_enemy);
            missile.InitMinion(this);
        }
    }
}
