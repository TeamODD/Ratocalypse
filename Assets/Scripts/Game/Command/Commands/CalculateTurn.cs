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
    public class CalculateTurn : Command, ICommandRequire<MapData>
    {
        private MapData _mapData;

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public CalculateTurn()
        {

        }

        public override ExecuteResult Execute()
        {
            var creatureData = Calcuate();
            if(creatureData == null)
            {
                return EndCommand(null);
            }
            var next = new SelectAndCastCard(creatureData);
            return new SubCommand(next);
        }

        public CreatureData Calcuate()
        {
            var placements = _mapData.GetPlacements();
            var creatureDatas = placements.Where(placement => {
                if(placement is CreatureData creatureData)
                {
                    return creatureData.CheckCastable();
                }
                return false;
            }).Cast<CreatureData>().ToList();
            if(creatureDatas.Count == 0)
            {
                return null;
            }
            creatureDatas.Sort((a, b) => b.Stamina.CompareTo(a.Stamina));
            return creatureDatas[0];
        }
    }
}