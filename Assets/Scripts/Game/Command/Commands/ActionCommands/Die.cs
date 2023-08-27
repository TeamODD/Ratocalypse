
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Die : TargetCommand<IDamageable>
    {
        private IDamageable _target;
        public Die(IDamageable target) : base(target)
        {
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            var animation = new Animation(_target, "Die", true, null, ()=>{});
            _target.Die();
            if(_target is Placement placement)
            {
                placement.Remove();
            }
            return new NextCommand(animation, WrapResult(true));
        }
    }
}