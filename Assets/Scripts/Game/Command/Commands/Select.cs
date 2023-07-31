using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
using TeamOdd.Ratocalypse.Card;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class Select : Command, IRequireSelectors, ICommandRequire<MapData>
    {
        static private Pattern _rook = new Pattern(new List<Vector2Int> { new Vector2Int(0, 1) 
        ,new Vector2Int(0, -1),new Vector2Int(1,0),new Vector2Int(-1, 0)});
        private ISelector _ratSelector;
        private ISelector _catSelector;

        private MapData _mapData;

        private MoveOrAttackRangeType _rangeType;
        private CreatureData _target;

        public void SetRequire((ISelector rat, ISelector cat) value)
        {
            _catSelector = value.cat;
            _ratSelector = value.rat;
        }

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public Select(MoveOrAttackRangeType rangeType, CreatureData target)
        {
            _rangeType = rangeType;
            _target = target;
        }

        public override ExecuteResult Execute()
        {
            ISelector selector = null;
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
            Selection selection = movement.CreateSelection((coords,i)=>{
                endWait(new Result { SelectedCoord = coords.GetCoord(i) });
            });
            selector.Select(selection, null);

            return result;
        }

        public class Result : ICommandResult
        {
            public Vector2Int SelectedCoord;
        }
    }
}