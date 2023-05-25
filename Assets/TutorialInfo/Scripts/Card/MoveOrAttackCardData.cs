namespace TeamOdd.Ratocalypse.Card
{
    public class MoveOrAttackCardData : CardData
    {
        public MoveOrAttackRangeType RangeType { get; protected set; }

        public MoveOrAttackCardData(string cardDataId, MoveOrAttackRangeType rangeType) : base(cardDataId)
        {
            RangeType = rangeType;
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