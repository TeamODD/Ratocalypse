using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.Command;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class CardCast : Command
    {
        private CreatureData _caster;
        private int _index = 0;
        private ICommandResult _parm;

        private CardCommand _cardCommand;

        public UnityEvent<ICommandResult> OnEnd { get; } = new UnityEvent<ICommandResult>();

        public CardCast(CreatureData caster, int index, CardCommand cardCommand)
        {
            _cardCommand = cardCommand;
            _caster = caster;
            _index = index;
            _parm = new CardCastData(caster, index);
        }

        private void SetParm(ICommandResult parm)
        {
            _parm = parm;
        }

        public override ExecuteResult Execute()
        {
            Command next = _cardCommand.Next(_parm);
            if (next == null)
            {
                return EndCommand(null);
            }
            next.RegisterOnEnd(SetParm);
            return new SubCommand(next);
        }
    }
}