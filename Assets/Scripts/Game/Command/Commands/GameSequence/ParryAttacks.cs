using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System.Linq;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using System.Collections.Generic;

namespace TeamOdd.Ratocalypse.GameLib.Commands.GameSequenceCommands
{
    public class ParryAttacks : Command
    {
        private List<TriggerCard> _cats;
        private List<TriggerCard> _rats;

        public ParryAttacks(List<TriggerCard> cats, List<TriggerCard> rats)
        {
            _cats = cats;
            _rats = rats;
        }

        public override ExecuteResult Execute()
        {
            int catDamages = 0;
            int ratDamages = 0;
            
            foreach(var cat in _cats)
            {
                catDamages += cat.CalculateFinalDamage();
            }

            foreach(var rat in _rats)
            {
                ratDamages += rat.CalculateFinalDamage();
            }

            if(catDamages < ratDamages)
            {
                var commands = _rats.Cast<Command>().ToList();
                return new NextCommands(commands);
            }
            else if(catDamages > ratDamages)
            {
                var commands = _cats.Cast<Command>().ToList();
                return new NextCommands(commands);
            }
            return new End();
        }
    }
}