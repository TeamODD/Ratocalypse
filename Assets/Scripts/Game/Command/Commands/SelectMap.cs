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
        private IMapSelector _ratSelector;
        private IMapSelector _catSelector;

        private MapData _mapData;

        private ChessRangeType _rangeType;
        private CreatureData _target;
        private MapSelecting _mapSelecting;

        private bool _selectTarget;
        private bool _selectMap;

        public void SetRequire((IMapSelector rat, IMapSelector cat) value)
        {
            _catSelector = value.cat;
            _ratSelector = value.rat;
        }

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public SelectMap(MapSelecting mapSelecting, CreatureData target, bool selectTarget, bool selectMap)
        {
            _mapSelecting = mapSelecting;
            _target = target;
            _selectTarget = selectTarget;
            _selectMap = selectMap;
        }

        public override ExecuteResult Execute()
        {
            IMapSelector selector = null;
            if (_target is RatData)
            {
                selector = _ratSelector;
            }
            else if (_target is CatData)
            {
                selector = _catSelector;
            }

            var (endWait, result) = CreateWait();
            _mapSelecting.Calculate(_mapData);
            if (_selectMap)
            {
                var coordSelection = _mapSelecting.CreateCoordSelection((coord) =>
                {
                    endWait(new End(new Result { SelectedCoord = coord }));
                });
                selector.Select(coordSelection);
            }

            if (_selectTarget)
            {
                var placementSelection = _mapSelecting.CreatePlacementSelection((placement) =>
                {
                    endWait(new End(new Result { SelectedPlacement = placement }));
                });
                selector.Select(placementSelection);
            }

            return result;
        }

        public class Result : ICommandResult
        {
            public Vector2Int? SelectedCoord = null;
            public Placement SelectedPlacement = null;
        }
    }
}