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