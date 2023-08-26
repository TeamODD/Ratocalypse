
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.CreatureLib.Attributes
{
    public interface IDamageable
    {
        public int MaxHp { get; }
        public int Hp { get; }
        public int Armor { get; }

        public void Die();
        public void ReduceHp(int amount);
        public void RestoreHp(int amount);
        public void IncreaseArmor(int amount);
        public void ReduceArmor(int amount);

        public bool IsAlive();

        public UnityEvent<int> OnHpReduced{ get; }
        public UnityEvent<int> OnHpRestored{ get; }
        public UnityEvent<int> OnArmorChanged{ get; }
        public UnityEvent OnDie{ get; }
    }
}