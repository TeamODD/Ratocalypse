using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.CardLib.CommandLib;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Attack : TargetCommand<IDamageable>
    {
        private int _damage = 0;
        private IAttackable _attacker;
        private IDamageable _target;

        public Attack(IAttackable attacker, IDamageable target, int damage) : base(target)
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