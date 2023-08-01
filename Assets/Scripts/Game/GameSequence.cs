using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.Card.Cards.Templates;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using UnityEngine;
using static TeamOdd.Ratocalypse.Card.Cards.Templates.MoveOrAttackCardData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class GameSequence
    {
        private MapData _mapData;
        private MapAnalyzer _mapAnalyzer;
        private CommandExecutor _commandExecutor;
        private List<Command> _commands;
        private GameStatistics _gameStatistics;


        private int _round = 1;

        public GameSequence(MapData mapData, CommandExecutor commandExecutor)
        {
            _mapData = mapData;
            _mapAnalyzer = new MapAnalyzer(_mapData);

            _commandExecutor = commandExecutor;
        }

        public void Start()
        {
            _commands = new List<Command>();
            var testCreature = (CreatureData)_mapData.GetPlacement(new Vector2Int(0, 0));
            MoveOrAttackCardData testCard = new MoveOrAttackCardData(0, new DataValue(), MoveOrAttackRangeType.Rook);
            var cardCast = new CardCast(testCreature, 0, testCard.CreateCardCommand());

            _commandExecutor.PushCommand(cardCast);
            _round = 1;
            _commandExecutor.Run();
        }

        public void CalculateTurn()
        {

        }
    }
}