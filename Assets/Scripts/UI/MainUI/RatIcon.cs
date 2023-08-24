using UnityEngine;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using UnityEngine.EventSystems;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatIcon : MonoBehaviour, IIcon
    {
        private Vector3 _startPosition;

        private ParticleSystem _fireEffect;

        public int Order { get => -_ratData.Stamina; }
        public Ease IconEase;
        public float _moveTime = 1f;

        private RatData _ratData;

        public void Awake()
        {
            _fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
            _fireEffect.Stop();
        }

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
        [ContextMenu("SetEffect")]
        public void SetEffect()
        {
            if (_fireEffect.isStopped)
            {
                _fireEffect.Play();
            }
            else
            {
                _fireEffect.Stop();
            }

        }
    }
}