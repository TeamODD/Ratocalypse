using System;

namespace TeamOdd.Ratocalypse.Card
{
    public class Deck
    {
        public DeckData DeckData { get; } = new DeckData();

        public CardData Draw(int index)
        {
            if (DeckData.Count == 0)
            {
                return null;
            }
            CardData cardData = DeckData.GetCard(index);
            DeckData.RemoveCard(index);
            return cardData;
        }

        public CardData DrawFirst()
        {
            return Draw(0);
        }

        public CardData DrawLast()
        {
            return Draw(Math.Max(0, DeckData.Count - 1));
        }
    }
}
