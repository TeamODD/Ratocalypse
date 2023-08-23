
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class RestoreStamina : TargetCommand<CreatureData>
    {
        private CreatureData _target;
        private int _amount = 0;
        public RestoreStamina(CreatureData target,int amount) : base(target)
        {
            _amount = amount;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            _target.RestoreStamina(_amount);
            return new End(WrapResult(true));
        }
    }
}