
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.DeckLib.DeckData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Draw : TargetCommand<CreatureData>
    {

        private CreatureData _target;
        private bool _fromTomb = false;
        private int _count = 0;
        public Draw(CreatureData target,int count,bool fromTomb) : base(target)
        {
            _target = target;
            _fromTomb = fromTomb;
            _count = count;
        }

        protected override ExecuteResult RunSuccess()
        {

            if(_fromTomb)
            {
                var (result, count) = _target.DeckData.DrawCardsFromTomb(_count);
            }
            else
            {
                var (result, count) = _target.DeckData.DrawCards(_count);
                if(result == DrawResult.Lack)
                {
                    _target.DeckData.ReviveCardToUndrawn();
                    _target.DeckData.DrawCards(count);
                }
            }
            return new End();
        }
    }
}