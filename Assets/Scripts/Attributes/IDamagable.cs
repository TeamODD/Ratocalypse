
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.CreatureLib.Attributes
{
    public interface IDamageable
    {
        public float MaxHp { get; }
        public float Hp { get; }

        public void Die();
        public void ReduceHp(float amount);
        public void RestoreHp(float amount);

        public UnityEvent<float> OnHpReduced{ get; }
        public UnityEvent<float> OnHpRestored{ get; }
        public UnityEvent OnDie{ get; }
    }
}