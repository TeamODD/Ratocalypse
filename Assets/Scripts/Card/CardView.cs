using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardView : MonoBehaviour
    {

        private CardData _cardData;


        public void Initialize(CardData cardData)
        {
            _cardData = cardData;
        }
    }
}