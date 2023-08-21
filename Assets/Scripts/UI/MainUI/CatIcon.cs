using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.UI
{
    public class CatIcon : MonoBehaviour, IIcon
    {
        private Vector3 _startPosition;

        private ParticleSystem _fireEffect;

        [field: SerializeField]
        public int Order { get; set; }

        public Ease IconEase;
        public float _moveTime = 2f;

        public void Awake()
        {
            _fireEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
            _fireEffect.Stop();
        }
        public void SetPosition(Vector3 position)
        {
            if (_fireEffect != null) {_fireEffect.transform.SetAsLastSibling(); }
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);
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