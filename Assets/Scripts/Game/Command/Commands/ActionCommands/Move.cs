using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CardLib.CommandLib;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Move : TargetCommand<CreatureData>
    {
        private ChessRangeType _rangeType;

        private CreatureData _creature;
        private Vector2Int _targetCoord;

        public Move(CreatureData creature, Vector2Int targetCoord) : base(creature)
        {
            _creature = creature;
            _targetCoord = targetCoord;
        }

        protected override ExecuteResult RunSuccess()
        {
            _creature.SetCoord(_targetCoord);
            return new End(WrapResult(true));
        }
    }
}