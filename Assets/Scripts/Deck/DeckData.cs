using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;
using System.Linq;

namespace TeamOdd.Ratocalypse.DeckLib
{
    [System.Serializable]
    public class DeckData
    {
        public enum DrawResult
        {
            Drawn,
            Exceed,
            Lack
        }

        private readonly List<int> _originDeckCards;

        public int MaxHandCount { get; private set; } = 10;

        [field: SerializeField]
        private HandData _handData = new HandData();
        private TombData _tombData = new TombData();
        private UndrawnCards _undrawnCards = new UndrawnCards();
        public CardColor CardColor { get; private set; } = CardColor.Blue;

        public DeckData(List<int> originDeckCards, CardColor cardColor, int maxHandCount = 10)
        {
            _originDeckCards = originDeckCards;
            MaxHandCount = maxHandCount;
            CardColor = cardColor;

            foreach (var id in _originDeckCards)
            {
                CardData newCardData = CardOriginData.Instance.CreateOriginCard(id);
                _undrawnCards.AddCard(newCardData);
            }
            _undrawnCards.Shuffle();
        }


        private DrawResult DrawCardFrom(CardDataCollection pool)
        {
            if (pool.Count == 0)
            {
                return DrawResult.Lack;
            }

            CardData cardData = pool.Draw();

            if (_handData.Count >= MaxHandCount)
            {
                _tombData.AddCard(cardData);
                return DrawResult.Exceed;
            }

            _handData.AddCard(cardData);
            return DrawResult.Drawn;
        }

        public DrawResult InsertHandAt(int index, CardData card)
        {
            if (_handData.Count >= MaxHandCount)
            {
                _tombData.AddCard(card);
                return DrawResult.Exceed;
            }

            _handData.InsertCard(index, card);
            return DrawResult.Drawn;
        }

        private (DrawResult result, int drawnCount) DrawsCardFrom(CardDataCollection pool, int count)
        {
            for (int i = 0; i < count; i++)
            {
                DrawResult result = DrawCardFrom(pool);
                if (result != DrawResult.Drawn)
                {
                    return (result, i);
                }
            }
            return (DrawResult.Drawn, count);
        }

        public (DrawResult result, int drawnCount) DrawCards(int count)
        {
            return DrawsCardFrom(_undrawnCards, count);
        }

        public (DrawResult result, int drawnCount) DrawCardsFromTomb(int count)
        {
            return DrawsCardFrom(_tombData, count);
        }

        public void AddCardToTomb(CardData cardData)
        {
            _tombData.AddCard(cardData);
            _tombData.Shuffle();
        }

        public void AddCardToUndrawn(CardData cardData)
        {
            _undrawnCards.AddCard(cardData);
            _undrawnCards.Shuffle();
        }

        public void ReviveCardToUndrawn()
        {
            if (_tombData.Count == 0)
            {
                return;
            }

            _undrawnCards.AddCards(_tombData.ToArray());
            _tombData.Clear();
            _undrawnCards.Shuffle();
        }



        public void RemoveCardFromHand(CardData cardData)
        {
            _handData.RemoveCard(cardData);
        }

        public CardData RemoveCardAtFromHand(int index)
        {
            return _handData.RemoveCard(index);
        }

        public void RemoveCardFromTomb(CardData cardData)
        {
            _tombData.RemoveCard(cardData);
        }

        public CardData RemoveCardAtFromTomb(int index)
        {
            return _tombData.RemoveCard(index);
        }

        public void RemoveCardFromUndrawn(CardData cardData)
        {
            _undrawnCards.RemoveCard(cardData);
        }

        public CardData RemoveCardAtFromUndrawn(int index)
        {
            return _undrawnCards.RemoveCard(index);
        }


        public List<CardData> GetHandCards()
        {
            return _handData.ToList();
        }

    }
}
