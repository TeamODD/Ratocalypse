
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class SetEffect : TargetCommand<IDamageable>
    {
        private CreatureData _target;
        private string _effect;
        private object _data;
        public SetEffect(CreatureData target, string effect, object data) : base(target)
        {
            _data = data;
            _effect = effect;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            _target.SetEffect(_effect,_data);
            return new End(WrapResult(true));
        }
    }
}