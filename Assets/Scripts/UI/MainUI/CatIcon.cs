using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.UI
{
    public class CatIcon : MonoBehaviour, IIcon
    {
        private Vector3 _startPosition;

        [field: SerializeField]
        public int Order { get; set; }
        public Ease IconEase;
        public float _moveTime = 2f;

        public void SetPosition(Vector3 position)
        {
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);

        }
    }
}