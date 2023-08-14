using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System.Collections.Generic;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class ParryAttacks : Command
    {
        private CastJointAttack.Result _cats;
        private CastJointAttack.Result _rats;

        public ParryAttacks(CastJointAttack.Result cats, CastJointAttack.Result rats)
        {
            _cats = cats;
            _rats = rats;
        }

        public override ExecuteResult Execute()
        {
            if(_cats.Damage < _rats.Damage)
            {
                var commands = _rats.TriggerCards.Cast<Command>().ToList();
                return new NextCommands(commands);
            }
            else if(_cats.Damage > _rats.Damage)
            {
                var commands = _cats.TriggerCards.Cast<Command>().ToList();
                return new NextCommands(commands);
            }
            return new End();
        }
    }
}