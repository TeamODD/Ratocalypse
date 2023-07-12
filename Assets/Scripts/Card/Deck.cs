namespace TeamOdd.Ratocalypse.Card
{
    public class Deck
    {
        public DeckData DeckData { get; } = new DeckData();

        public CardDataValue Draw(int index)
        {
            if (DeckData.Count == 0)
            {
                return null;
            }
            CardDataValue cardData = DeckData.GetCardDataValue(index);
            DeckData.RemoveCard(index);
            return cardData;
        }

        public CardDataValue DrawFirst()
        {
            return Draw(0);
        }

        public CardDataValue DrawLast()
        {
            return Draw(DeckData.Count - 1);
        }
    }
}
