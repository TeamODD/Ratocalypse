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

        private readonly List<(int, CardDataValue)> _originDeckCards;

        public int MaxHandCount { get; private set; } = 10;

        [field: SerializeField]
        private HandData _handData = new HandData();
        private TombData _tombData = new TombData();
        private UndrawnCards _undrawnCards = new UndrawnCards();

        private (int index,CardData cardData) ?_castCardData = null;

        public DeckData(List<(int, CardDataValue)> originDeckCards, int maxHandCount = 10)
        {
            _originDeckCards = originDeckCards;
            MaxHandCount = maxHandCount;

            foreach (var (id, dataValue) in _originDeckCards)
            {
                CardData newCardData = CardOriginData.Instance.CreateOriginCard(id);
                newCardData.DeckDataValue = dataValue;
                _undrawnCards.AddCard(newCardData);
            }
            _undrawnCards.Shuffle();
        }

        public DrawResult DrawCard()
        {
            if (_undrawnCards.Count == 0)
            {
                return DrawResult.Lack;
            }

            CardData cardData = _undrawnCards.Draw();

            if (_handData.Count >= MaxHandCount)
            {
                _tombData.AddCard(cardData);
                return DrawResult.Exceed;
            }

            _handData.AddCard(cardData);
            return DrawResult.Drawn;
        }

        public (DrawResult result, int drawnCount) DrawCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                DrawResult result = DrawCard();
                if (result != DrawResult.Drawn)
                {
                    return (result, i);
                }
            }
            return (DrawResult.Drawn, count);
        }

        // public int DrawFromTomb(int count)
        // {
        //     for (int i = 0; i < count; i++)
        //     {
        //         _handData.AddCard(_tombData.Draw());
        //     }
        // }

        public CardData CastCard(int index)
        {
            CardData castCardData = _handData.RemoveCard(index);
            _castCardData = (index, castCardData);
            return castCardData;
        }

        public int TriggerCard()
        {
            if (_castCardData == null)
            {
                throw new System.InvalidOperationException("No card is casting");
            }
            var (index, cardData) = _castCardData.Value;
            _tombData.AddCard(cardData);
            var cost = cardData.GetCost();
            _castCardData = null;
            return cost;
        }

        public void CancelCast()
        {
            var (index, cardData) = _castCardData.Value;
            _handData.InsertCard(index,cardData);
            _castCardData = null;
        }

        public List<(int index, CardData card)> GetCastableCards(int stamina)
        {
            var castableCards = new List<(int, CardData)>();
            for (int i = 0; i < _handData.Count; i++)
            {
                CardData cardData = _handData.GetCard(i);
                if (cardData.GetCost() <= stamina)
                {
                    castableCards.Add((i, cardData));
                }
            }
            return castableCards;
        }

        public bool HasCastableCard(int stamina)
        {
            foreach (CardData cardData in _handData)
            {
                if (cardData.GetCost() <= stamina)
                {
                    return true;
                }
            }
            return false;
        }

        public List<CardData> GetHandCards()
        {
            return _handData.ToList();
        }



    }
}
