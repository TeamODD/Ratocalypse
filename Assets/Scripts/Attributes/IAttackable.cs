using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.CreatureLib.Attributes
{
    public interface IAttackable
    {
        public void Attack(IDamageable target, int damage);
    }
}