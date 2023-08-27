using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Damage : TargetCommand<IDamageable>
    {
        private IDamageable _target;
        private int _damage = 0;

        public Damage(IDamageable target, int damage) : base(target)
        {
            _target = target;
            _damage = damage;
        }

        protected override ExecuteResult RunSuccess()
        {
            if (_target is CreatureData creatureData)
            {
                var (hasPoison, stack) = creatureData.HasEffect("Poison");
                if (hasPoison)
                {
                    _damage += (int)stack;
                }

                var (hasGod, _) = creatureData.HasEffect("God");
                if (hasGod)
                {
                    creatureData.RemoveEffect("God");
                    return new End();
                }

                var (hasProtect, data) = creatureData.HasEffect("Protect");
                if (hasProtect)
                {
                    var protector = data as CreatureData;
                    if (protector != null && protector.IsAlive())
                    {
                        creatureData.RemoveEffect("Protect");
                        return new NextCommand(new Damage(protector, _damage));
                    }
                }
            }
            _target.ReduceHp(_damage);
            List<Command> commands = new List<Command>();

            commands.Add(new Animation(_target, "Hit", true, null, () => { }));

            if (_target.Hp<=0)
            {
                commands.Add(new Die(_target));
            }

            return new NextCommands(commands, WrapResult(true));
        }
    }
}