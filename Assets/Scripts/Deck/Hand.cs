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
    public class Hand : MonoBehaviour, ISelector<HandData>
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


        private HandData _handData = new HandData();

        [SerializeField]
        private List<HandCard> _cards = new List<HandCard>();
        [SerializeField]
        private Stack<HandCard> _deactiveCards = new Stack<HandCard>();

        private HandCard _focusingCard = null;

        [SerializeField]
        private MovementValues _movementValues = new MovementValues();

        [ReadOnly, SerializeField]
        private bool _isSelecting = false;

        private Selection<HandData> _selection;

        public void PreCreateCards()
        {
            for (int i = 0; i < _handData.MaxCount; i++)
            {
                CardView card = _cardFactory.Create(new CardData(0, new CardDataValue()), transform);
                HandCard handCard = card.gameObject.AddComponent<HandCard>();
                card.gameObject.AddComponent<CardEvents>();
                card.gameObject.SetActive(false);
                _deactiveCards.Push(handCard);
            }
        }

        public void AddCard(CardData cardData)
        {
            HandCard card = _deactiveCards.Pop();
            ResetCard(card, cardData);
            _cards.Add(card);
            UpdatePosition();
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
            cardView.View(cardData);
        }

        private void Drag(HandCard card)
        {
            if(!_isSelecting)
            {
                return;
            }
            card.Run(CardAction.StartDrag);
        }


        [ContextMenu("AddOne")]
        public void AddOne()
        {
            AddCard(new CardData(0, new CardDataValue()));
        }

        public void Execute(HandCard card)
        {
            int index = _cards.IndexOf(card);
            _selection.Select(index);
            _selection = null;
            _isSelecting = false;
            UnFocus(card);
            _cards.Remove(card);
            _deactiveCards.Push(card);
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
            if(_cards.Count == 0)
            {
                return false;
            }

            UnFocus(_cards[0]);
            _cards[0].GetComponent<CardEvents>().RemoveAllListeners();

            _deactiveCards.Push(_cards[0]);
            _cards[0].gameObject.SetActive(false);
            _cards.RemoveAt(0);
            return true;
        }

        public void Awake()
        {
            PreCreateCards();
        }

        public void Update()
        {
            UpdateFocus();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                AddOne();
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                Remove();
            }
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
            float maxIndex = _cards.Count - 1;
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
            for (int i = 0; i < _cards.Count; i++)
            {
                var (position, rotation) = GetTransfrom(i);
                _cards[i].SetOrigin(position, rotation);
                _cards[i].Run(CardAction.UpdateOrigin);
            }
        }

        public void UpdateHandData(HandData handData)
        {
            _handData = handData;
            UpdateCards();
        }

        public void UpdateCards()
        {
            _isSelecting = false;
            while(Remove()){};
            for (int i = 0; i < _handData.Count; i++)
            {
                AddCard(_handData.GetCard(i));
            }
            UpdatePosition();
        }

        public void Select(Selection<HandData> selection)
        {
            UpdateHandData(selection.GetCandidates());
            _selection = selection;
            _isSelecting = true;
        }
        
    }
}
