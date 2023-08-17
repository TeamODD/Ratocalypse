using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class SelectMap : Command, IRequireMapSelectors, ICommandRequire<MapData>
    {
        static private Pattern _rook = new Pattern(new List<Vector2Int> { new Vector2Int(0, 1) 
        ,new Vector2Int(0, -1),new Vector2Int(1,0),new Vector2Int(-1, 0)});
        private IMapSelector _ratSelector;
        private IMapSelector _catSelector;

        private MapData _mapData;

        private ChessRangeType _rangeType;
        private CreatureData _target;

        public void SetRequire((IMapSelector rat, IMapSelector cat) value)
        {
            _catSelector = value.cat;
            _ratSelector = value.rat;
        }

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public SelectMap(ChessRangeType rangeType, CreatureData target)
        {
            _rangeType = rangeType;
            _target = target;
        }

        public override ExecuteResult Execute()
        {
            IMapSelector selector = null;
            if(_target is RatData)
            {
                selector = _ratSelector;
            }
            else if(_target is CatData)
            {
                selector = _catSelector;
            }

            var (endWait, result) = CreateWait();

            DirectionalMovement movement = new DirectionalMovement(_target, _mapData, _rook);
            movement.Calculate();

            var coordSelection = movement.CreateCoordSelection((coord)=>{
                endWait(new End(new Result { SelectedCoord = coord }));
            });
            
            var placementSelection = movement.CreatePlacementSelection((placement)=>{
                endWait(new End(new Result {SelectedPlacement = placement }));
            });
            
            selector.Select(coordSelection);
            selector.Select(placementSelection);            
            return result;
        }

        public class Result : ICommandResult
        {
            public Vector2Int SelectedCoord;
            public Placement SelectedPlacement = null;
        }
    }
}