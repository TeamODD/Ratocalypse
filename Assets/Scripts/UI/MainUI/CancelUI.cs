using UnityEngine;
using System.Collections;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using UnityEngine.EventSystems;
using TeamOdd.Ratocalypse.CardLib;

namespace TeamOdd.Ratocalypse.UI
{
    public class CancelUI:MonoBehaviour
    {
        [SerializeField]
        private Vector2 _hidePosition;
        [SerializeField]
        private Vector2 _outPosition;

        [SerializeField]
        private float _time;
        [SerializeField]
        private Ease _ease;

        [SerializeField]
        private CardView _cardView;

        public void SetCardView(CardData cardData, DeckLib.CardColor cardColor)
        {
            _cardView.View(cardData, cardColor);
        }

        public void SetView(bool view)
        {
            if (view)
            {
                transform.DOLocalMove(_outPosition, _time).SetEase(_ease);
            }
            else
            {
                transform.DOLocalMove(_hidePosition, 0.1f).SetEase(_ease);
            }
        }

    }
}