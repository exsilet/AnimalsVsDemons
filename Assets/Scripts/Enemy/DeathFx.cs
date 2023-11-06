using UnityEngine;

namespace Enemy
{
    public class DeathFx : MonoBehaviour
    {
        private const string DieFx = "DieFx";        

        private Animator _animator;                

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }        

        public void PlayDeath()
        {
            _animator.Play(DieFx);
        }
        
        public void DestroyFx()
        {
            Destroy(gameObject);
        }
    }
}

