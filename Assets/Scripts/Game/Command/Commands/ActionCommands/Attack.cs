using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using System;
using TeamOdd.Ratocalypse.CreatureLib;
using System.Collections.Generic;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Attack : Command, IRequireExtraExcutor
    {
        private Func<CommandExecutor> _createExecutor;
        private int _damage = 0;
        private IAttackable _attacker;
        private List<IDamageable> _targets;

        public void SetRequire(Func<CommandExecutor> require)
        {
            _createExecutor = require;
        }

        public Attack(IAttackable attacker, IDamageable target, int damage)
        {
            _damage = damage;
            _attacker = attacker;
            _targets = new List<IDamageable>() { target };
        }

        public Attack(IAttackable attacker, List<IDamageable> targets, int damage)
        {
            _damage = damage;
            _attacker = attacker;
            _targets = targets;
        }

        public override ExecuteResult Execute()
        {
            if (_attacker is IDamageable attacker && attacker.IsAlive() == false)
            {
                return new End();
            }

            var (endWait, result) = CreateWait();
            var attackExecutor = _createExecutor();

            var count = _targets.Count + 1;

            List<CommandExecutor> damageExecutors = new List<CommandExecutor>();
            foreach (var target in _targets)
            {
                var damageExecutor = _createExecutor();
                damageExecutors.Add(damageExecutor);
                damageExecutor.OnEmpty.AddListener(EndExecute);

                var damage = new Damage(target, _damage);
                damageExecutor.PushCommand(damage);
            }

            Animation animation = new Animation(_attacker, "Attack", true, null,
            () =>
            {
                damageExecutors.ForEach(damageExecutor => damageExecutor.Run());
            }, () => { });

            attackExecutor.PushCommand(animation);
            attackExecutor.Run();
            attackExecutor.OnEmpty.AddListener(EndExecute);


            void EndExecute()
            {
                count--;
                if (count == 0)
                {
                    if (_attacker is CreatureData creatureData)
                    {
                        var (has, _) = creatureData.HasEffect("Mozzarella");
                        if (has)
                        {
                            endWait(new NextCommand(new Heal(creatureData, _damage)));
                            return;
                        }
                    }
                    endWait(new End());
                }
            }

            return result;
        }
    }
}