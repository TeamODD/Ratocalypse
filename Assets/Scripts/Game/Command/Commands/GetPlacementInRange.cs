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
    public class GetPlacementInRange : Command, ICommandRequire<MapData>
    {
        private MapData _mapData;
        private List<Vector2Int> _range = new List<Vector2Int>();
        private Func<Placement,bool> _filter;
        private MapSelecting _mapSelecting;

        public void SetRequire(MapData require)
        {
            
            _mapData = require;
        }

        public GetPlacementInRange(MapSelecting selecting)
        {
            _mapSelecting = selecting;
        }


        public override ExecuteResult Execute()
        {
            _mapSelecting.Calculate(_mapData);
            var result = new Result()
            {
                placements = _mapSelecting.PlacementCanditates
            };
            return new End(result);
        }

        public class Result : ICommandResult
        {
            public List<Placement> placements;
        }
    }
}