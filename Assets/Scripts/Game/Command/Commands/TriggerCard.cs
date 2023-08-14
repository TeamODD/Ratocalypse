using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using static TeamOdd.Ratocalypse.CardLib.CommandLib.CardCommand;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class TriggerCard : Command
    {
        private CreatureData _caster;
        private int _index = 0;
        private ICommandResult _parm;

        private CardCommand _cardCommand;
        private CardData _cardData;

        public TriggerCard(CardCommand cardCommand,ICommandResult parm)
        {
            _cardCommand = cardCommand;
            _parm = parm;
        }

        private void SetParm(ICommandResult parm)
        {
            _parm = parm;
        }

        public override ExecuteResult Execute()
        {
            var (cardCommandtype, command) = _cardCommand.Next(_parm);
            if (cardCommandtype == CardCommandType.EndTrigger)
            {
                return new End(null);
            }
            
            command.RegisterOnEnd(SetParm);
            return new SubCommand(command);
        }
    }
}