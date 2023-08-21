using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using System;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Attack : TargetCommand<IDamageable>, IRequireExtraExcutor
    {
        private Func<CommandExecutor> _createExecutor;
        private int _damage = 0;
        private IAttackable _attacker;
        private IDamageable _target;

        public void SetRequire(Func<CommandExecutor> require)
        {
            _createExecutor = require;
        }

        public Attack(IAttackable attacker, IDamageable target, int damage) : base(target)
        {
            _damage = damage;
            _attacker = attacker;
            _target = target;
        }

        public override ExecuteResult Execute()
        {
            var (endWait, result) = CreateWait();
            
            var attackExecutor = _createExecutor();
            var damageExecutor = _createExecutor();

            var count = 2;

            void EndExecute()
            {
                count--;
                if (count == 0)
                {
                    endWait(new End());
                }
            }

            Animation animation = new Animation(_attacker, "Attack", true, null,
            () =>{
                var damage = new Damage(_target, _damage);
                damageExecutor.PushCommand(damage);
                damageExecutor.Run();
                damageExecutor.OnEmpty.AddListener(EndExecute);
            }, ()=>{});

            attackExecutor.PushCommand(animation);
            attackExecutor.Run();
            attackExecutor.OnEmpty.AddListener(EndExecute);

            return result;
        }
    }
}