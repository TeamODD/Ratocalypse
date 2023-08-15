using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using System;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;

namespace TeamOdd.Ratocalypse.GameLib.Commands.GameSequenceCommands
{
    public class CastJointAttack : Command
    {
        private List<CreatureData> _creaturesLeft;
        private CreatureData _next;

        private List<TriggerCard> _triggerCards = new List<TriggerCard>();
        private bool _runTrigger;

        public CastJointAttack(List<CreatureData> creatureDatas, bool runTrigger)
        {
            _runTrigger = runTrigger;
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
                int count = _triggerCards.Count;
                float multiplier = count == 1? 1 : 1.2f + (count * 0.2f);
                int totalDamage = 0;
                foreach (var trigger in _triggerCards)
                {
                    trigger.DamageMultiplier = multiplier;
                    totalDamage += trigger.CalculateFinalDamage();
                }

                if(_runTrigger)
                {
                    var commands = _triggerCards.Cast<Command>().ToList();
                    return new NextCommands(commands);
                }

                var executeResult = new Result
                {
                    TriggerCards = _triggerCards,
                    Damage = totalDamage
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
                    var castCard = creatureData.CastCard(selectedInex, false);
                    castCard.RegisterOnEnd((result)=>{
                        var triggerCommand = result as TriggerCard;
                        CastCallback(currentIndex, triggerCommand);
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
            public int Damage;
        }
    }
}