
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Heal : TargetCommand<IDamageable>
    {
        private IDamageable _target;
        private int _amount = 0;
        public Heal(IDamageable target,int amount) : base(target)
        {
            _amount = amount;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            _target.RestoreHp(_amount);
            return new End(WrapResult(true));
        }
    }
}