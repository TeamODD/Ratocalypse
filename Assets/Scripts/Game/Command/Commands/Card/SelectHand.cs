using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands
{
    public class SelectHand : Command
    {
        private ICardSelector _ratSelector;
        private ICardSelector _catSelector;

        private CreatureData _creatureData;

        public SelectHand(CreatureData creatureData)
        {
            _creatureData = creatureData;
        }

        public override ExecuteResult Execute()
        {
            var (endWait, result) = CreateWait();
            var indicies = _creatureData.GetCastableCardIndices();
            var selection = new Selection<List<int>>(indicies,
            (int index)=>{
                var cardCastData = new CardCastData(_creatureData,index);
                endWait(new End(cardCastData));
            });
            _creatureData.SetCardSlection(selection);
            _creatureData.SelectCard();
            return result;
        }


    }
}