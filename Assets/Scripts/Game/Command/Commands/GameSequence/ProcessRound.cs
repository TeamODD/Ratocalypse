using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.UI;
using static TeamOdd.Ratocalypse.DeckLib.DeckData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.GameLib.Commands.GameSequenceCommands
{
    public class ProcessRound : Command, ICommandRequire<MapData>, ICommandRequire<GameStatistics>
    {

        private MapData _mapData;
        private GameStatistics _gameStatistics;

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public void SetRequire(GameStatistics require)
        {
            _gameStatistics = require;
        }


        public override ExecuteResult Execute()
        {
            DrawAndRestoreAll();
            _gameStatistics.round++;
            return new SubCommand(new CalculateTurn());
        }

        public void DrawAndRestoreAll()
        {
            var placements = _mapData.GetPlacements();
            foreach(var placement in placements)
            {
                if(placement is CatData cat)
                {
                    var (drawResult, count) = cat.DeckData.DrawCards(3);
                    if(drawResult==DrawResult.Lack)
                    {
                        cat.DeckData.ReviveCardToUndrawn();
                        cat.DeckData.DrawCards(count);
                    }

                    cat.RestoreAllStamina();
                }
                else if(placement is RatData rat)
                {
                    var (drawResult, count) = rat.DeckData.DrawCards(1);
                    if(drawResult==DrawResult.Lack)
                    {
                        rat.DeckData.ReviveCardToUndrawn();
                        rat.DeckData.DrawCards(count);
                    }
                    rat.RestoreAllStamina();
                }

                if(placement is CreatureData creatureData)
                {
                    creatureData.RemoveEffect("Poison");
                    creatureData.RemoveEffect("Taunt");
                    creatureData.ReduceStrength(1);
                }
            }
            
        }



    }
}