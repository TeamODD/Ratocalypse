
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class IncreaseMaxStamina : TargetCommand<CreatureData>
    {
        private CreatureData _target;
        private int _amount = 0;
        public IncreaseMaxStamina(CreatureData target,int amount) : base(target)
        {
            _amount = amount;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            _target.IncreaseMaxStamina(_amount);
            return new End(WrapResult(true));
        }
    }
}