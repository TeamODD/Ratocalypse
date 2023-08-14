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
    public class CastCard : Command
    {
        private CreatureData _caster;
        private int _index = 0;
        private ICommandResult _parm;

        private CardCommand _cardCommand;
        private CardData _cardData;

        public CastCard(CardCastData cardCastData)
        {
            _index = cardCastData.CardIndex;
            _caster = cardCastData.Caster;
            _cardData = _caster.DeckData.GetHandCards()[_index];
            _parm = cardCastData;
        }

        private void SetParm(ICommandResult parm)
        {
            _parm = parm;
        }

        public override ExecuteResult Execute()
        {
            if(_cardCommand == null)
            {
                _cardCommand = _cardData.CreateCardCommand();
                _caster.CastCard(_index);
            }

            var (cardCommandtype, command) = _cardCommand.Next(_parm);

            if(cardCommandtype == CardCommandType.EndCast)
            {
                _caster.TriggerCard();
                TriggerCard triggerCommand = new TriggerCard(_cardCommand, _parm);
                return new End(new Result() { TriggerCommand = triggerCommand });
            }

            command.RegisterOnEnd(SetParm);
            return new SubCommand(command);
        }

        public class Result:ICommandResult
        {
            public TriggerCard TriggerCommand; 
        }
    }
}