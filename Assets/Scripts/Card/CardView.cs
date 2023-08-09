using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections.Generic;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardView : MonoBehaviour
    {
        [SerializeField]
        private Renderer _cardImage;

        [SerializeField]
        private Renderer _cardBackground;
        [SerializeField]
        private List<Texture2D> _cardBackgrounds;

        
        public void View(CardData cardData)
        {
            _cardImage.material.SetTexture("_Texture", cardData.Texture);
            SetBackground(cardData.CardType);
        }

        public void SetBackground(int index)
        {
            _cardBackground.material.SetTexture("_Texture", _cardBackgrounds[index]);
        }
    }
}