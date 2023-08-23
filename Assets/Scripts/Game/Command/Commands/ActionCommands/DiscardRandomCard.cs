
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class DiscardRandomCard : TargetCommand<CreatureData>
    {
        private CreatureData _target;
        private int _count = 0;
        public DiscardRandomCard(CreatureData target, int count) : base(target)
        {
            _count = count;
            _target = target;
        }

        protected override ExecuteResult RunSuccess()
        {
            List<CardData> cards = new List<CardData>();
            for(int i = 0; i < _count; i++)
            {
                var count = _target.DeckData.GetHandCards().Count;
                if(count == 0)
                {
                    break;
                }
                var index = Random.Range(0, count);
                cards.Add(_target.DeckData.RemoveCardAtFromHand(index));
            }
            InnerResult innerResult = new InnerResult()
            {
                DiscardCards = cards
            };
            
            return new End(WrapResult(true, innerResult));
        }

        public class InnerResult : ICommandResult
        {
            public List<CardData> DiscardCards = new List<CardData>();
        } 
    }
}