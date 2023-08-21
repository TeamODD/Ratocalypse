using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.DeckLib;
using TMPro;


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

        [SerializeField]
        private TextMeshPro _title;

        [SerializeField]
        private TextMeshPro _description;
        
        public void View(CardData cardData, CardColor cardType)
        {
            _cardImage.material.SetTexture("_Texture", cardData.Texture);
            SetBackground((int)cardType);
            SetTitle(cardData.GetTitle());
            SetDescription(cardData.GetDescription());
        }

        public void SetBackground(int index)
        {
            _cardBackground.material.SetTexture("_Texture", _cardBackgrounds[index]);
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }
    }
}