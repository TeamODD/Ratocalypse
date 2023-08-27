using UnityEngine;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatIcon : MonoBehaviour, IIcon, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _startPosition;

        [SerializeField]
        private RawImage _headIcon;

        [SerializeField]
        private ParticleSystem _fireEffect;

        public int Order { get => _ratData.HasCastableCard()?-_ratData.Stamina:1; }
        public Ease IconEase;
        public float _moveTime = 1f;

        [SerializeField]
        private float _scaleTime = 0.2f;
        [SerializeField]
        private float _scaleSize = 1.2f;

        private Vector3 _originScale;

        public bool Selected = false;

        private RatData _ratData;

        public void Awake()
        {
            _originScale = transform.localScale;
            
        }

        public void Initialize(RatData ratData, Texture2D headIcon)
        {
            _ratData = ratData;
            _headIcon.texture = headIcon;
            _ratData.OnTriggerdCard.AddListener(()=>SetEffect(true));
        }

        public void SetPosition(int order, Vector3 position)
        {
            if(order == 0)
            {

            }
            SetEffect(false);
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ratData.SelectCard();
        }
 
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale( _originScale*_scaleSize, _scaleTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_originScale, _scaleTime);
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