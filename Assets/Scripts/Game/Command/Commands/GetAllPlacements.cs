using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class GetAllPlacements : Command, ICommandRequire<MapData>
    {
        private MapData _mapData;
        private Func<Placement,bool> _filter;
        
        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public GetAllPlacements(Func<Placement,bool> filter = null)
        {
            _filter = filter;
        }


        public override ExecuteResult Execute()
        {
            var result = new Result();
            if(_filter==null)
            {
                result.placements = _mapData.GetPlacements();
            }
            else
            {
                result.placements = _mapData.GetPlacements().Where(_filter).ToList();
            }
            
            return new End(result);
        }

        public class Result : ICommandResult
        {
            public List<Placement> placements;
        }
    }
}