using TeamOdd.Ratocalypse.CardLib.Command;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardData
    {
        public int Id { get; }

        public CardDataValue OriginDataValue;
        public CardDataValue DeckDataValue = new CardDataValue();
        public CardDataValue GameDataValue = new CardDataValue();

        public CardData(int id, CardDataValue originDataValue)
        {
            Id = id;
            OriginDataValue = originDataValue;
        }

        public virtual CardData CloneOriginCard()
        {
            CardData cloned = new CardData(Id, OriginDataValue);
            return cloned;
        }

        public virtual CardData CloneDeckCard()
        {
            CardData cloned = CloneOriginCard();
            cloned.DeckDataValue = DeckDataValue.Clone();
            return cloned;
        }

        public virtual CardData CloneGameCard()
        {
            CardData cloned = CloneDeckCard();
            cloned.GameDataValue = GameDataValue.Clone();
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

        public virtual int GetCost()
        {
            return OriginDataValue.Cost + DeckDataValue.Cost + GameDataValue.Cost;
        }
    }

    public class CardDataValue
    {
        public int Cost { get; private set; }

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