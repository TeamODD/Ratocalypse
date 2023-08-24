using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands
{
    public class SelectAndCastCard : ChainCommand
    {
        private CreatureData _creature;
        private bool _runTrigger;

        public SelectAndCastCard(CreatureData creature, bool runTrigger)
        {
            _runTrigger = runTrigger;
            _creature = creature;

            AddCommand((_)=>{
                return new SelectHand(_creature);
            });
            AddCommand((result)=>{
                CardCastData cardCastData = result as CardCastData;
                return creature.CastCard(cardCastData.CardIndex, _runTrigger);
            });
        }

    }
}