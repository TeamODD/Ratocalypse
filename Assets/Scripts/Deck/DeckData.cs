using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.DeckLib
{
    [System.Serializable]
    public class DeckData
    {
        private readonly List<(int, CardDataValue)> _originDeckCards;
        
        [field: SerializeField]
        public HandData HandData{get; private set;} = new HandData();
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
                _undrawnCards.Shuffle();
            }
        }

        public void DrawCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                HandData.AddCard(_undrawnCards.Draw());
            }
        }
    }
}
