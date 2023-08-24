
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class IncreaseStamina : TargetCommand<CreatureData>
    {
        private CreatureData _target;
        private int _amount = 0;
        private bool _bypassMax = false;
        public IncreaseStamina(CreatureData target,int amount,bool bypassMax = false) : base(target)
        {
            _bypassMax = bypassMax;
            _amount = amount;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            _target.RestoreStamina(_amount, _bypassMax);
            return new End(WrapResult(true));
        }
    }
}