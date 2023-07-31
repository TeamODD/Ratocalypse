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
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class Move : Command
    {
        private MoveOrAttackRangeType _rangeType;

        public UnityEvent<ICommandResult> OnEnd { get; } = new UnityEvent<ICommandResult>();

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
            return EndCommand(null);
        }


    }
}