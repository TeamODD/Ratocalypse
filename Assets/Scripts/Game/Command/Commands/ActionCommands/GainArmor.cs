
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class GainArmor : TargetCommand<IDamageable>
    {
        private CreatureData _target;
        private int _amount = 0;
        public GainArmor(CreatureData target,int amount) : base(target)
        {
            _amount = amount;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            _target.IncreaseArmor(_amount);
            return new End(WrapResult(true));
        }
    }
}