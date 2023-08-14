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
using TeamOdd.Ratocalypse.UI;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class CalculateTurn : Command, ICommandRequire<MapData>, ICommandRequire<TurnUI>
    {
        private MapData _mapData;
        private TurnUI _turnUI;

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public void SetRequire(TurnUI require)
        {
            _turnUI = require;
        }

        public CalculateTurn()
        {

        }

        public override ExecuteResult Execute()
        {
            var orderedDatas = Calcuate();

            if (orderedDatas.Count == 0)
            {
                return new End();//게임 끝 draw
            }

            List<CreatureData> next = null;
            foreach(var creatureDatas in orderedDatas)
            {
                if(creatureDatas.Any(creatureData => creatureData.HasCastableCard()))
                {
                    next = creatureDatas;
                    continue;
                }
            }

            if(next==null)
            {
                return new End();//턴끝
            }

            if(next.Count == 1)
            {
                return new SubCommand(new SelectAndTriggerCard(next.First()));
            }
            else
            {
                return new SubCommand(new ProcessMultipleTurns(next));
            }
        }

        public List<List<CreatureData>> Calcuate()
        {
            var placements = _mapData.GetPlacements();
            var creatureDatas = placements.Where(placement =>
            {
                return placement is CreatureData;
            }).Cast<CreatureData>().ToList();


            creatureDatas.Sort((a, b) =>
            {
                return a.Stamina - b.Stamina;
            });
            var orders = new List<List<CreatureData>>();
            var prevStamina = -1;
            foreach (var creatureData in creatureDatas)
            {
                if (creatureData.Stamina != prevStamina)
                {
                    orders.Add(new List<CreatureData>());
                    prevStamina = creatureData.Stamina;
                }
                orders.Last().Add(creatureData);
            }
            _turnUI.UpdatePositions();
            return orders;
        }


    }
}