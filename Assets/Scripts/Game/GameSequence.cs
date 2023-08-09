using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CardLib.Cards.Templates;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using UnityEngine;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class GameSequence
    {
        private MapData _mapData;
        private MapAnalyzer _mapAnalyzer;
        private CommandExecutor _commandExecutor;
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
            _mapData.GetPlacements().ForEach((placement)=>{
                if(placement is CreatureData creatureData)
                {
                    creatureData.DeckData.DrawCards(1);
                }
            });
            _round = 1;
            _commandExecutor.PushCommand(new CalculateTurn());
            _commandExecutor.Run();
        }
    }
}