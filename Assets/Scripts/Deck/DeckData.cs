using System.Collections.Generic;
using TeamOdd.Ratocalypse.Card;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Deck
{

    public class DeckData
    {
        private readonly List<(int, CardDataValue)> _originDeckCards;
        
        private HandData _deckData = new HandData();
        private TombData _tombData = new TombData();
        private UndrawnCards _undrawnCards = new UndrawnCards();

        public DeckData(List<(int, CardDataValue)> originDeckCards)
        {
            _originDeckCards = originDeckCards;
            
            foreach (var (id, dataValue) in _originDeckCards)
            {
                CardData newCardData = CardOriginData.Instance.CreateOriginCard(id);
                newCardData.DeckDataValue = dataValue;
                _undrawnCards.AddCard(newCardData);
            }
        }
    }
}
