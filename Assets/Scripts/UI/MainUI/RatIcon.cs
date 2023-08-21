using UnityEngine;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatIcon : MonoBehaviour, IIcon
    {
        private Vector3 _startPosition;

        [SerializeField]
        private ParticleSystem _fireEffect;
        [field: SerializeField]
        public int Order { get; set; }
        public Ease IconEase;
        public float _moveTime = 2f;

        public void SetPosition(Vector3 position)
        {
            if (_fireEffect!=null) { Debug.Log("적용"); _fireEffect.transform.SetAsLastSibling(); }
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);
        }
        public void OnEffect()
        {
            _fireEffect.Play();

        }
    }
}