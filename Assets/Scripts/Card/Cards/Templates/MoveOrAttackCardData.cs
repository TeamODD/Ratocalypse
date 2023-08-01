using TeamOdd.Ratocalypse.Card.Command;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Card.Cards.Templates
{
    public class MoveOrAttackCardData : CardData
    {
        public MoveOrAttackCardData(int cardDataId, CardDataValue originDataValue, MoveOrAttackRangeType rangeType) : base(cardDataId, originDataValue)
        {
            DeckDataValue = new DataValue();
        }

        public MoveOrAttackRangeType RangeType { get; protected set; }

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
        public class DataValue : CardDataValue
        {
            public MoveOrAttackRangeType RangeType;

            public DataValue() : base(0)
            {
                RangeType = MoveOrAttackRangeType.None;
            }

            public DataValue(int cost, MoveOrAttackRangeType rangeType) : base(cost)
            {
                RangeType = rangeType;
            }

            public override CardDataValue Clone()
            {
                return new DataValue(Cost, RangeType);
            }
        }

        public enum MoveOrAttackRangeType
        {
            King,
            Queen,
            Rook,
            Bishop,
            Knight,
            Pawn,
            None,
        }
    }
}