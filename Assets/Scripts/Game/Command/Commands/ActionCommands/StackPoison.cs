
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class StackPoison : TargetCommand<CreatureData>
    {
        private CreatureData _target;
        private int _stack = 0 ;
        public StackPoison(CreatureData target, int stack) : base(target)
        {
            _stack = stack;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            var (has,prevStack) = _target.HasEffect("Poison");
            if(has)
            {
                _stack += (int)prevStack;
            }
            _target.SetEffect("Poison", _stack);
            return new End(WrapResult(true));
        }
    }
}