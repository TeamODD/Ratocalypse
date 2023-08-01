using TeamOdd.Ratocalypse.Card.Command;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;

namespace TeamOdd.Ratocalypse.Card
{
    public class CardData
    {
        public int CardDataId { get; }

        public CardDataValue OriginDataValue;
        public CardDataValue DeckDataValue = new CardDataValue();
        public CardDataValue GameDataValue = new CardDataValue();

        public CardData(int cardDataId, CardDataValue originDataValue)
        {
            CardDataId = cardDataId;
            OriginDataValue = originDataValue;
        }

        public virtual CardData Clone()
        {
            CardData cloned = new CardData(CardDataId, OriginDataValue.Clone())
            {
                DeckDataValue = DeckDataValue
            };
            return cloned;
        }

        public virtual CardCommand CreateCardCommand()
        {
            return new CardCommand((CardCastData data) => { return null; });
        }

        public virtual string GetTitle()
        {
            return "Card";
        }

        public virtual string GetDescription()
        {
            return "Description";
        }
    }

    public class CardDataValue
    {
        public int Cost { get; private set; }
        public string Title { get; private set; }

        public CardDataValue()
        {
            Cost = 0;
        }

        public CardDataValue(int cost)
        {
            Cost = cost;
        }

        public void SetCost(int cost)
        {
            Cost = cost;
        }


        public virtual CardDataValue Clone()
        {
            return new CardDataValue(Cost);
        }
    }
}