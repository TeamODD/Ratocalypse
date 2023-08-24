using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using static TeamOdd.Ratocalypse.DeckLib.DeckData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class InsertCard : TargetCommand<CreatureData>
    {
        private CreatureData _target;
        private int _cardId = 0;

        public InsertCard(CreatureData target, int cardId) : base(target)
        {
            _target = target;
            _cardId = cardId;
        }

        protected override ExecuteResult RunSuccess()
        {
            var card = CardOriginData.Instance.CreateOriginCard(_cardId);
            _target.DeckData.InsertLastHand(card);
            return new End(WrapResult(true));
        }
    }
}