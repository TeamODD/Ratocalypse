using UnityEngine;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using UnityEngine.EventSystems;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatIcon : MonoBehaviour, IIcon, IPointerClickHandler
    {
        private Vector3 _startPosition;

        public int Order { get => -_ratData.Stamina;}
        public Ease IconEase;
        public float _moveTime = 1f;

        private RatData _ratData;

        public void Initialize(RatData ratData)
        {
            _ratData = ratData;
        }

        public void SetPosition(Vector3 position)
        {
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ratData.SelectCard();
        }
    }
}