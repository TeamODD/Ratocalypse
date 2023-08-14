using UnityEngine;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using System;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class CastJointAttack : Command
    {
        private List<CreatureData> _creaturesLeft;
        private CreatureData _next;

        private List<TriggerCard> _triggerCards = new List<TriggerCard>();

        public CastJointAttack(List<CreatureData> creatureDatas)
        {
            _creaturesLeft = creatureDatas;
        }

        private Action<ExecuteResult> _endWait;
        public override ExecuteResult Execute()
        {
            var (endWait, result) = CreateWait();
            _endWait = endWait;

            SetNext();

            if (_creaturesLeft.Count == 0)
            {
                var executeResult = new Result
                {
                    TriggerCards = _triggerCards
                };
                return new End(executeResult);
            }

            return result;
        }

        public void SetNext()
        {
            int last = _creaturesLeft.Count - 1;
            for (int i = last; i >= 0; i--)
            {
                var creatureData = _creaturesLeft[i];
                creatureData.RemoveSelection();
                if (!creatureData.HasCastableCard())
                {
                    _creaturesLeft.RemoveAt(i);
                    continue;
                }
                var indices = creatureData.GetCastableCardIndices();
                var currentIndex = i;
                var selection = new Selection<List<int>>(indices, (int selectedInex) =>
                {
                    var cardCastData = new CardCastData(creatureData, selectedInex);
                    var castCard = new CastCard(cardCastData);
                    castCard.RegisterOnEnd((commandResult)=>{
                        var castCardResult = commandResult as CastCard.Result;
                        CastCallback(currentIndex, castCardResult.TriggerCommand);
                    });
                    _endWait(new SubCommand(castCard));
                });
                creatureData.SetCardSlection(selection);
            }
            if(_creaturesLeft.Count > 0)
            {
                _creaturesLeft.First().SelectCard();
            }
        }

        public void CastCallback(int creatureIndex, TriggerCard triggerCard)
        {
            _creaturesLeft.RemoveAt(creatureIndex);
            _triggerCards.Add(triggerCard);
        }

        public class Result : ICommandResult
        {
            public List<TriggerCard> TriggerCards;
            public int Damage = 0;
        }
    }
}