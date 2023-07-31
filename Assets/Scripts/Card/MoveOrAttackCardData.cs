using TeamOdd.Ratocalypse.Card.Command;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Card
{
    public class MoveOrAttackCardData : CardData
    {
        public MoveOrAttackRangeType RangeType { get; protected set; }

        public MoveOrAttackCardData(long cardDataId, MoveOrAttackRangeType rangeType) : base(cardDataId)
        {
            RangeType = rangeType;
        }

        public override CardData Clone()
        {
            return new MoveOrAttackCardData(CardDataId, RangeType);
        }

        public override CardCommand CreateCardCommand()
        {
            CreatureData caster = null;
            CardCommand cardCommand = new CardCommand((CardCastData data) =>
            {
                caster = data.Caster;
                return new Select(MoveOrAttackRangeType.King, data.Caster);
            });

            cardCommand.AddCommand((result) =>
            {
                Select.Result selectResult = (Select.Result)result;
                return new Move(caster, selectResult.SelectedCoord);
            });

            return cardCommand;
        }
    }

    public enum MoveOrAttackRangeType
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn
    }
}