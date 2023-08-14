using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.DataCommands;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib.Cards.Templates
{
    
    public class MoveOrAttackCardData : CardData
    {
        public MoveOrAttackCardData(Texture2D texture, int cardDataId, CardDataValue originDataValue, int cardType) 
        : base(cardDataId, originDataValue, texture, cardType)
        {
            DeckDataValue = new DataValue();
        }

        public MoveOrAttackRangeType RangeType { get; protected set; }


        public override CardData CloneOriginCard()
        {
            CardData cloned = new MoveOrAttackCardData(Texture, _cardDataId, OriginDataValue, CardType);
            return cloned;
        }

        public override CardCommand CreateCardCommand()
        {
            CreatureData caster = null;
            CardCommand cardCommand = new CardCommand((CardCastData data) =>
            {
                caster = data.Caster;
                var temp = new SelectMap(MoveOrAttackRangeType.King, data.Caster);
                return temp;
            });

            cardCommand.AddTriggerCommand((result) =>
            {
                SelectMap.Result selectResult = result as SelectMap.Result;
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