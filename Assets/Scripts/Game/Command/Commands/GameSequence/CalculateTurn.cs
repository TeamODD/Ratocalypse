using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.UI;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.GameLib.Commands.GameSequenceCommands
{
    public class CalculateTurn : Command, ICommandRequire<MapData>, ICommandRequire<TurnUI>
    {
        private MapData _mapData;
        private TurnUI _turnUI;

        public void SetRequire(MapData require)
        {
            _mapData = require;
        }

        public void SetRequire(TurnUI require)
        {
            _turnUI = require;
        }

        public CalculateTurn()
        {

        }

        public override ExecuteResult Execute()
        {
            var ratCount = _mapData.GetPlacements().Where(placement => placement is RatData).Count();
            var catCount = _mapData.GetPlacements().Where(placement => placement is CatData).Count();
            if(ratCount == 0)
            {
                return new NextCommands(new List<Command>(){
                    new EventAnimation("Defeat"),
                    new LoadNewGame()}
                );
            }
            else if(catCount == 0)
            {
                return new NextCommands(new List<Command>(){
                    new EventAnimation("victory"),
                    new LoadNewGame()}
                );
            }

            var orderedDatas = Calcuate();

            if (orderedDatas.Count == 0)
            {
                return new NextCommands(new List<Command>(){
                    new EventAnimation("defeat"),
                    new LoadNewGame()}
                );
            }

            List<CreatureData> next = null;
            foreach(var creatureDatas in orderedDatas)
            {
                if(creatureDatas.Any(creatureData => creatureData.HasCastableCard()))
                {
                    next = creatureDatas;
                    next = next.Where(creatureData => creatureData.HasCastableCard()).ToList();
                    continue;
                }
            }

            if(next==null)
            {
                return new NextCommand(
                    new EventAnimation("nextround")
                );
            }

            next.ForEach(creatureData =>
            {
                var (has,data) = creatureData.HasEffect("Stealth");
                if(has)
                {
                    if((int)data == 0)
                    {
                        creatureData.RemoveEffect("Stealth");
                    }
                    creatureData.SetEffect("Stealth", (int)data - 1);
                }
                
            });

            if(next.Count == 1)
            {
                var target = next.First();
                return new SubCommand(new SelectAndCastCard(target, true));
            }
            else
            {

                return new SubCommand(new ProcessMultipleTurns(next));
            }
        }

        public List<List<CreatureData>> Calcuate()
        {
            var placements = _mapData.GetPlacements();
            var creatureDatas = placements.Where(placement =>
            {
                return placement is CreatureData;
            }).Cast<CreatureData>().ToList();


            creatureDatas.Sort((a, b) =>
            {
                return a.Stamina - b.Stamina;
            });
            var orders = new List<List<CreatureData>>();
            var prevStamina = -1;
            foreach (var creatureData in creatureDatas)
            {
                if (creatureData.Stamina != prevStamina)
                {
                    orders.Add(new List<CreatureData>());
                    prevStamina = creatureData.Stamina;
                }
                orders.Last().Add(creatureData);
            }
            _turnUI.UpdatePositions();
            return orders;
        }


    }
}