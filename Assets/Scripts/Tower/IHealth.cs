using UnityEngine.Events;

namespace Tower
{
    public interface IHealth
    {
        event UnityAction HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(int damage);
    }
}