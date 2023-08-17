using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.DataCommands
{
    public class Attack : Command
    {
        private int _damage = 0;
        private IAttackable _attacker;
        private IDamageable _target;

        public Attack(IAttackable attacker, IDamageable target,int damage)
        {
            _damage = damage;
            _attacker = attacker;
            _target = target;
        }

        public override ExecuteResult Execute()
        {
            _target.ReduceHp(_damage);
            return new End();
        }
    }
}