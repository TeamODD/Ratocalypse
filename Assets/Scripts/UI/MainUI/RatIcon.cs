using UnityEngine;
using DG.Tweening;


namespace TeamOdd.Ratocalypse.UI
{
    public class RatIcon : MonoBehaviour, IIcon
    {
        private Vector3 _startPosition;

        private ParticleSystem _fireEffect;

        [SerializeField]
        private Ease _iconEase = Ease.InOutCubic;

        [field: SerializeField]
        public int Order { get; set; }
       
        public float _moveTime = 1f;

        public void Awake()
        {
            _fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
            _fireEffect.Stop();
        }
        public void SetPosition(Vector3 position)
        {
            if (_fireEffect!=null) { _fireEffect.transform.SetAsLastSibling(); }
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(_iconEase);
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