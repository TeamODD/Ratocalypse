using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.DeckLib
{
    public class Hand : MonoBehaviour
    {
        [SerializeField]
        private CardFactory _cardFactory;

        private HandData _handData = new HandData();
        
        public void AddCard(CardData cardData)
        {
            Card card = _cardFactory.Create(cardData, transform);
            card.transform.localPosition = Vector3.zero;
        }


    }
}
