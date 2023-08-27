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
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.GameLib.Commands.GameSequenceCommands
{
    public class CastJointAttack : Command, ICommandRequire<MapData>
    {
        private List<CreatureData> _creaturesLeft;
        private CreatureData _next;

        private MapData _mapData;


        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        private List<TriggerCard> _triggerCards = new List<TriggerCard>();
        private bool _runTrigger;

        public CastJointAttack(List<CreatureData> creatureDatas, bool runTrigger)
        {
            _runTrigger = runTrigger;
            _creaturesLeft = creatureDatas;
        }
        private int _nextIndex = 0;
        private Action<ExecuteResult> _endWait;
        public override ExecuteResult Execute()
        {
            var (endWait, result) = CreateWait();
            _endWait = endWait;

            SetNext();

            if (_creaturesLeft.Count == 0)
            {
                _triggerCards.ForEach((trigger) =>
                {
                    if (trigger.Destenation != null)
                    {
                        foreach (var placement in _mapData.GetPlacements())
                        {
                            if(placement is TempPlacement)
                            {
                                placement.Remove();
                            }
                        }
                    }
                });

                int count = _triggerCards.Count;
                float multiplier = count == 1 ? 1 : 1.2f + (count * 0.2f);
                int totalDamage = 0;
                foreach (var trigger in _triggerCards)
                {
                    trigger.DamageMultiplier = multiplier;
                    totalDamage += trigger.CalculateFinalDamage();
                }

                if (_runTrigger)
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

            _triggerCards.ForEach((trigger) =>
            {
                if (!trigger.Caster.IsAlive() && trigger.Destenation != null)
                {
                    foreach (var coord in trigger.Caster.Shape.GetCoords(trigger.Destenation.Value))
                    {
                        var tempPlacement = _mapData.GetPlacement(coord);
                        if (tempPlacement != null&& tempPlacement is TempPlacement)
                        {
                            tempPlacement.Remove();
                        }
                    }

                }
            });

            _triggerCards = _triggerCards.Where((trigger) => trigger.Caster.IsAlive()).ToList();
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
                    castCard.RegisterOnEnd((result) =>
                    {
                        if (result == null)
                        {
                            _creaturesLeft.RemoveAt(currentIndex);
                            return;
                        }
                        else if (result is CastCard.Result castResult)
                        {
                            if (castResult.Cancel)
                            {
                                _nextIndex = currentIndex;
                                return;
                            }
                            CastCallback(currentIndex, castResult.TriggerCard);
                        }


                    });
                    _endWait(new SubCommand(castCard));
                });
                creatureData.SetCardSlection(selection);
            }
            if (_creaturesLeft.Count > 0)
            {
                _creaturesLeft[_nextIndex].SelectCard();
                _nextIndex = 0;
            }
        }

        public void CastCallback(int creatureIndex, TriggerCard triggerCard)
        {
            if (triggerCard.Destenation != null)
            {
                foreach (var coord in triggerCard.Caster.Shape.GetCoords(triggerCard.Destenation.Value))
                {
                    if (_mapData.GetPlacement(coord) == null)
                    {
                        new TempPlacement(_mapData, coord, new Shape(new List<Vector2Int>{Vector2Int.zero}));
                    }
                }

            }
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