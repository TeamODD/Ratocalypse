using UnityEngine;
using System.Collections;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using UnityEngine.EventSystems;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine.UIElements;
using TeamOdd.Ratocalypse.TestScripts;

namespace TeamOdd.Ratocalypse.UI
{
    public class CatIcon : MonoBehaviour, IIcon, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _startPosition;

        [SerializeField]
        private ParticleSystem _fireEffect;

        public int Order { get => _catData.HasCastableCard() ? -_catData.Stamina : 1; }
        public Ease IconEase;
        public float _moveTime = 2f;

        [SerializeField]
        private float _scaleTime = 0.2f;
        [SerializeField]
        private float _scaleSize = 1.2f;

        private Vector3 _originScale;

        private CatData _catData;

        [SerializeField]
        private CardView _cardView;


        [SerializeField]
        private Ease _cardEase;

        [SerializeField]
        private float _cardScale;

        [SerializeField]
        private Vector3 _cardOffset;

        private TileSelector _tileSelector;

        private bool _turn = false;
        private CardData _currentCardData;
        public void Awake()
        {
            _originScale = transform.localScale;
        }

        public void SetPosition(int order, Vector3 position)
        {
            _turn = order == 0;
            if (_turn)
            {
                var cardIndices = _catData.GetCastableCardIndices();
                if (cardIndices.Count > 0)
                {
                    _currentCardData = _catData.DeckData.GetHandCards()[cardIndices[0]];
                    _cardView.View(_currentCardData, _catData.DeckData.CardColor);
                }
                else
                {
                    _turn = false;
                }
            }
            SetEffect(false);
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);
        }

        public void Initialize(CatData catData, TileSelector tileSelector)
        {
            _tileSelector = tileSelector;
            _catData = catData;
            _catData.OnTriggerdCard.AddListener(() => SetEffect(true));
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (_tileSelector.Selecting)
            {
                return;
            }
            if (_turn)
            {
                var selection = _currentCardData.GetPreview(_catData);
                _tileSelector.Preview(selection);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_tileSelector.Selecting)
            {
                return;
            }
            _tileSelector.Reset();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale( _originScale*_scaleSize, _scaleTime);
            if (_turn)
            {
                _cardView.transform.DOScale(_cardScale, _scaleTime);
                _cardView.transform.DOLocalMove(_cardOffset, _scaleTime);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_originScale, _scaleTime);
            _cardView.transform.DOScale(0, _scaleTime);
            _cardView.transform.DOLocalMove(Vector3.zero, _scaleTime);

            if (_tileSelector.Selecting)
            {
                return;
            }
            _tileSelector.Reset();
        }


        [ContextMenu("SetEffect")]
        public void SetEffect(bool play)
        {
            if (play)
            {
                _fireEffect.Play();
            }
            else
            {
                _fireEffect.Stop();
            }

        }

        public void Remove()
        {
            gameObject.SetActive(false);
        }

    }
}