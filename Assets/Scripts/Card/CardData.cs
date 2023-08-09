using TeamOdd.Ratocalypse.CardLib.Command;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    [System.Serializable]
    public class CardData
    {
        public int Id { get; }

        public CardDataValue OriginDataValue;
        public CardDataValue DeckDataValue = new CardDataValue();
        public CardDataValue GameDataValue = new CardDataValue();
        protected int _cardDataId;

        public Texture2D Texture{ get; private set; }
        public int CardType { get; private set; } = 0;

        public CardData(int id, CardDataValue originDataValue,Texture2D texture, int cardType)
        {
            Id = id;
            CardType = cardType;
            Texture = texture;
            OriginDataValue = originDataValue;
        }

        public CardData(int cardDataId, CardDataValue originDataValue)
        {
            _cardDataId = cardDataId;
            OriginDataValue = originDataValue;
        }

        public virtual CardData CloneOriginCard()
        {
            CardData cloned = new CardData(Id, OriginDataValue, Texture, CardType);
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
    [System.Serializable]
    public class CardDataValue
    {
        public int Cost;

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