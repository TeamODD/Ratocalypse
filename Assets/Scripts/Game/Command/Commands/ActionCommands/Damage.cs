using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using System;
using System.Collections.Generic;

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
            _target.ReduceHp(_damage);
            List<Command> commands = new List<Command>();

            commands.Add(new Animation(_target, "Hit", true, null, ()=>{}));

            if(_target.IsAlive() == false)
            {
                commands.Add(new Die(_target));
            }

            return new NextCommands(commands,WrapResult(true));
        }
    }
}