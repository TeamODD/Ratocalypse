using System;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    [Serializable]
    public class CardData
    {
        public int Id;
        public CardValueData OriginValueData;
        public CardValueData GameValueData;

        public Texture2D Texture{ get; private set; }

        public CardData()
        {

        }

        public static CardData CreateCardData(Type cardDataType, int id, Texture2D texture, CardValueData originValueData)
        {
            CardData cardData = Activator.CreateInstance(cardDataType) as CardData;

            cardData.Id = id;
            cardData.Texture = texture;
            cardData.OriginValueData = originValueData;
            cardData.GameValueData = cardData.CreateDefaultValueData();
            return cardData;
        }

        public CardValueData CreateDefaultValueData()
        {
            var type = OriginValueData.GetType();
            return Activator.CreateInstance(type) as CardValueData;
        }

        public virtual CardData CloneOriginCard()
        {
            CardData cloned = MemberwiseClone() as  CardData;
            cloned.GameValueData = CreateDefaultValueData();
            return cloned;
        }

        public virtual CardData CloneGameCard()
        {
            CardData cloned = CloneOriginCard();
            cloned.GameValueData = GameValueData.Clone();
            return cloned;
        }

        public virtual CastCard CreateCastCardCommand(CardCastData cardCastData, bool runTrigger)
        {
            return new CastCard(cardCastData, runTrigger);
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
            return OriginValueData.Cost + GameValueData.Cost;
        }
    }

    public class CardValueData
    {
        public int Cost = 0;

        public CardValueData Clone()
        {
            return MemberwiseClone() as CardValueData;
        }
    }
}