using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;
using DG.Tweening;
using static TeamOdd.Ratocalypse.DeckLib.HandCard;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using System;

namespace TeamOdd.Ratocalypse.DeckLib
{
    public class Hand : MonoBehaviour, ICardSelector
    {
        [SerializeField]
        private CardFactory _cardFactory;

        [Header("Hand Shape")]
        [SerializeField]
        private float _maxWidth = 3;
        [SerializeField]
        private float _height = 1;
        [SerializeField]
        private float _circleRadius = 5f;
        [SerializeField]
        private float _maxGapAngle = 0.1f;


        [Header("Card Distance")]
        [SerializeField]
        private float _verticalGap = 0.1f;
        [SerializeField]
        private float _focusHeight;
        [SerializeField]
        private float _dragHeight;


        private DeckData _deckData;

        [SerializeField]
        private List<HandCard> _handcards = new List<HandCard>();
        [SerializeField]
        private Queue<HandCard> _deactiveCards = new Queue<HandCard>();

        private HandCard _focusingCard = null;

        [SerializeField]
        private MovementValues _movementValues = new MovementValues();

        [ReadOnly, SerializeField]
        private bool _isSelecting = false;

        private Selection<List<int>> _selection;


        public void PreCreateCards()
        {
            for (int i = 0; i <= 20; i++)
            {
                CardView card = _cardFactory.Create(new CardData(), transform, CardColor.Blue);
                HandCard handCard = card.gameObject.AddComponent<HandCard>();
                card.gameObject.AddComponent<CardEvents>();
                card.gameObject.SetActive(false);
                _deactiveCards.Enqueue(handCard);
            }
        }

        public HandCard AddCard(CardData cardData)
        {
            HandCard card = _deactiveCards.Dequeue();
            ResetCard(card, cardData);
            _handcards.Add(card);
            UpdatePosition();
            return card;
        }

        private void ResetCard(HandCard card, CardData cardData)
        {
            card.Initialize(_movementValues);
            card.gameObject.SetActive(true);
            card.OnExecute.AddListener(Execute);
            CardEvents cardEvents = card.GetComponent<CardEvents>();
            cardEvents.MouseOverEvents.AddListener(() => SetFocus(card));
            cardEvents.MouseOutEvents.AddListener(() => UnFocus(card));

            cardEvents.MouseDownEvents.AddListener(() => Drag(card));
            cardEvents.MouseUpEvents.AddListener(() => card.Run(CardAction.EndDrag));

            CardView cardView = card.GetComponent<CardView>();
            cardView.View(cardData,_deckData.CardColor);
        }

        private void Drag(HandCard card)
        {
            if(!_isSelecting)
            {
                return;
            }
            var index = _handcards.IndexOf(card);

            if(_selection.GetCandidates().Contains(index))
            {
                card.Run(CardAction.StartDrag);
            }
        }

        public void Execute(HandCard card)
        {
            int index = _handcards.IndexOf(card);
            _selection.Select(index);
            _selection = null;
            _isSelecting = false;
            UnFocus(card);
            _handcards.Remove(card);
            _deactiveCards.Enqueue(card);
            card.Run(CardAction.Consume);
            card.GetComponent<CardEvents>().RemoveAllListeners();
            card.gameObject.SetActive(false);
            UpdatePosition();
        }

        public void SetFocus(HandCard card)
        {
            if(_focusingCard != card)
            {
                UnFocus(_focusingCard);
            }
            card.Run(CardAction.Focus);
            _focusingCard = card;
        }

        public void UnFocus(HandCard card)
        {
            if (_focusingCard == null)
            {
                return;
            }
            if (_focusingCard == card)
            {
                card.Run(CardAction.UnFocus);
                _focusingCard = null;
            }
        }

        public void UpdateFocus()
        {
            _focusingCard?.Run(CardAction.Focus);
        }

        [ContextMenu("Remove")]
        public bool Remove()
        {
            if(_handcards.Count == 0)
            {
                return false;
            }

            UnFocus(_handcards[0]);
            _handcards[0].GetComponent<CardEvents>().RemoveAllListeners();

            _deactiveCards.Enqueue(_handcards[0]);
            _handcards[0].gameObject.SetActive(false);
            _handcards.RemoveAt(0);
            return true;
        }

        public void Awake()
        {
            PreCreateCards();
        }


        private float CalcInCircleRatio(float ratio)
        {
            return ratio * _maxWidth / (_circleRadius * 2);
        }

        private float CalcYRatio(float xRatio)
        {
            return Mathf.Cos(xRatio * Mathf.PI);
        }

        private float CalcXRatio(float xRatio)
        {
            return Mathf.Sin(xRatio * Mathf.PI);
        }


        public (Vector3 position,float angle) GetTransfrom(int index)
        {
            float maxIndex = _handcards.Count - 1;
            float maxAngle = CalcInCircleRatio(0.5f);
            float minY = CalcYRatio(maxAngle);
            float minX = CalcXRatio(maxAngle);

            float angle = (index - (maxIndex / 2f)) * Mathf.Min(_maxGapAngle, 2 * maxAngle / maxIndex);
            float posZ = _verticalGap * (index - (maxIndex / 2f));
            float posY = (CalcYRatio(angle) - minY) / (1 - minY) * _height;

            float posX = CalcXRatio(angle) * (_maxWidth / minX);
            float rotation = -angle * Mathf.Rad2Deg;

            return (new Vector3(posX, posY, posZ), rotation);
        }

        [ContextMenu("UpdatePosition")]
        public void UpdatePosition()
        {
            for (int i = 0; i < _handcards.Count; i++)
            {
                var (position, rotation) = GetTransfrom(i);
                _handcards[i].SetOrigin(position, rotation);
                _handcards[i].Run(CardAction.UpdateOrigin);
            }
        }

        public void UpdateHandCards(DeckData deckData)
        {
            _deckData = deckData;
            UpdateCards();
        }

        public void UpdateCards()
        {
            _isSelecting = false;
            while(Remove()){};
            var newCards = _deckData.GetHandCards();
            for (int i = 0; i < newCards.Count; i++)
            {
                var handCard = AddCard(newCards[i]);
                handCard.GetComponent<CardGlow>().SetInactiveGlow();
            }
            UpdatePosition();
        }

        public void Select(Selection<List<int>> selection)
        {
            _selection = selection;
            foreach(int index in _selection.GetCandidates())
            {
                _handcards[index].GetComponent<CardGlow>().SetActiveGlow();
            }
            _isSelecting = true;
        }

        public void SetTarget(DeckData deckData)
        {
            UpdateHandCards(deckData);
        }

        public DeckData GetTarget()
        {
            return _deckData;
        }

    }
}
