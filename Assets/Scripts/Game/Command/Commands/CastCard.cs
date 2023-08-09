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
            _cardData = _caster.DeckData.HandData.GetCard(_index);
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
                _caster.DeckData.HandData.RemoveCard(_index);
                _caster.UseStamina(_cardData.GetCost());
                //TODO: creaturedata에서 stamina처리 -> 이어서 DeckData에서 cast, cancel 처리, 
            }

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