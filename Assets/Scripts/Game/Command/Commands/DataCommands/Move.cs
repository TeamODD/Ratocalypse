using UnityEngine;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.DataCommands
{
    public class Move : Command
    {
        private MoveOrAttackRangeType _rangeType;

        private CreatureData _creature;
        private Vector2Int _targetCoord;

        public Move(CreatureData creature, Vector2Int targetCoord)
        {
            _creature = creature;
            _targetCoord = targetCoord;
        }

        public override ExecuteResult Execute()
        {
            _creature.SetCoord(_targetCoord);
            return new End();
        }
    }
}