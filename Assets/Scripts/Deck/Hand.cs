using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;
using DG.Tweening;
using static TeamOdd.Ratocalypse.DeckLib.HandCard;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using System;
using TeamOdd.Ratocalypse.TestScripts;
using TeamOdd.Ratocalypse.CreatureLib;

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
        private CreatureData _caster;

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

        private bool _deckFocused = false;


        [SerializeField]
        private float _unFocusedScale = 0.5f;
        [SerializeField]
        private Vector3 _unfocusedOffset = new Vector2(0, 0);
    
        [SerializeField]
        private Ease _focusEase = Ease.OutBack;
        [SerializeField]
        private float _focusTime = 0.3f;

        private Vector3 _originPosition;
        private float _originMaxAngle;
        private float _originHeight;

        [SerializeField]
        private float _unfocusedRatio = 0.5f;


        [SerializeField]
        private float _changeDeckTime = 0.5f;
        [SerializeField]
        private Ease _changeDeckEase = Ease.OutBack;
        [SerializeField]
        private float _changeDeckHeight = -1f;

        [SerializeField]
        private TileSelector _tilePreviewer;
        

        [ContextMenu("ToggleFocus")]
        public void ToggleFocus()
        {
            SetDeckFocused(!_deckFocused);
        }

        public void Update()
        {
            if(Input.GetKeyDown("space"))
            {
                ToggleFocus();
            }
        }

        public void SetDeckFocused(bool focus)
        {
            if(focus == _deckFocused)
            {
                return;
            }
            _deckFocused = focus;

            if(focus)
            {
                var dest = _originPosition + _unfocusedOffset;
                transform.DOLocalMove(dest, _focusTime).SetEase(_focusEase);
                transform.DOScale(_unFocusedScale, _focusTime).SetEase(_focusEase);
                _maxGapAngle = _unfocusedRatio * _maxGapAngle;
                UpdatePosition();
            }
            else
            {
                transform.DOLocalMove(_originPosition, _focusTime).SetEase(_focusEase);
                transform.DOScale(1, _focusTime).SetEase(_focusEase);
                _maxGapAngle = _originMaxAngle;
                UpdatePosition();
            }
        }



        public void PreCreateCards()
        {
            for (int i = 0; i <= 20; i++)
            {
                CardView card = _cardFactory.CreateDummy(transform);
                HandCard handCard = card.gameObject.AddComponent<HandCard>();
                card.gameObject.AddComponent<CardEvents>();
                card.gameObject.SetActive(false);
                _deactiveCards.Enqueue(handCard);
            }
        }


        public HandCard InsertCard(CardData cardData, int index)
        {
            HandCard card = _deactiveCards.Dequeue();
            ResetCard(card, cardData);
            _handcards.Insert(index, card);
            UpdatePosition();
            return card;
        }

        public HandCard AddCard(CardData cardData)
        {
            return InsertCard(cardData, _handcards.Count);
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
            cardEvents.MouseUpEvents.AddListener(() => EndDrag(card));

            CardView cardView = card.GetComponent<CardView>();
            cardView.View(cardData,_deckData.CardColor);
        }

        private void EndDrag(HandCard card)
        {
            _tilePreviewer.Reset();
            card.Run(CardAction.EndDrag);
        }

        private void Drag(HandCard card)
        {
            if(!_isSelecting)
            {
                return;
            }
            var index = _handcards.IndexOf(card);
            var cardData = _deckData.GetHandCards()[index];

            _tilePreviewer.Preview(cardData.GetPreview(_caster));

            if(_selection.GetCandidates().Contains(index))
            {
                card.Run(CardAction.StartDrag);
            }
        }

        public void Execute(HandCard card)
        {
            _isSelecting = false;
            int index = _handcards.IndexOf(card);
            UnFocus(card);
            _handcards.Remove(card);
            _deactiveCards.Enqueue(card);
            card.Run(CardAction.Consume);
            card.GetComponent<CardEvents>().RemoveAllListeners();
            card.gameObject.SetActive(false);
            UpdatePosition();
            var prevSelection = _selection;
            _selection = null;
            prevSelection.Select(index);
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
            _originMaxAngle = _maxGapAngle;
            _originPosition = transform.localPosition;
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
        public void UpdatePosition(bool force = false)
        {
            for (int i = 0; i < _handcards.Count; i++)
            {
                var (position, rotation) = GetTransfrom(i);
                _handcards[i].SetOrigin(position, rotation);
                if(force)
                {
                    _handcards[i].ForceUpdateOrigin();
                }
                else
                {
                    _handcards[i].Run(CardAction.UpdateOrigin);
                }
            }
        }

        public void AddCardCallback(CardData cardData)
        {
            AddCard(cardData);
        }
        public void InsertCardCallback(CardData cardData, int index)
        {
            InsertCard(cardData, index);
        }


        public void UpdateHandCards(DeckData deckData,Action callback = null)
        {
            if(_deckData == deckData)
            {
                SetDeckFocused(true);
                callback?.Invoke();
                return;
            }

            if(_deckData != null)
            {
                _deckData.OnCardDrawn.RemoveListener(AddCardCallback);
                _deckData.OnCardInsert.RemoveListener(InsertCardCallback);
            }

            _deckData = deckData;

            _deckData.OnCardDrawn.AddListener(AddCardCallback);
            _deckData.OnCardInsert.AddListener(InsertCardCallback);
            var dest = transform.localPosition - _changeDeckHeight * Vector3.up;
            transform.DOLocalMove(dest, _changeDeckTime).SetEase(_changeDeckEase).OnComplete(() =>
            {
                _maxGapAngle = _originMaxAngle;
                transform.localScale = Vector3.one;
                transform.localPosition = _originPosition - _changeDeckHeight * Vector3.up;
                _deckFocused = true;
                UpdateCards();
                transform.DOLocalMove(_originPosition, _changeDeckTime).SetEase(_changeDeckEase).OnComplete(()=>{
                    callback?.Invoke();
                });
            });
        }

        public void UpdateCards(bool force = true)
        {
            _isSelecting = false;
            while(Remove()){};
            var newCards = _deckData.GetHandCards();
            for (int i = 0; i < newCards.Count; i++)
            {
                var handCard = AddCard(newCards[i]);
                handCard.GetComponent<CardGlow>().SetInactiveGlow();
            }

            UpdatePosition(force);
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

        public void SetTarget(CreatureData caster, Action callback = null)
        {
            _caster = caster;
            UpdateHandCards(caster.DeckData, callback);
        }

        public DeckData GetTarget()
        {
            return _deckData;
        }

    }
}
