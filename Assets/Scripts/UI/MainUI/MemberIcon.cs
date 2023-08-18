using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.UI
{

    public class MemberIcon : MonoBehaviour
    {
        private Vector3 _startPosition;
        [SerializeField]
        private TextMeshProUGUI _textTMP;
        [SerializeField]
        private Image _ratIcon;
        [SerializeField]
        private float _xMoveTime = 1f;
        [SerializeField]
        private float _yMoveTime = 2f;
        public Ease IconEase;

        public bool Activation = true;
        public void SetMember(Vector3 position)
        {
            _startPosition = transform.localPosition;
            var mX = DOTween.To(() => _startPosition.x, x => transform.localPosition = new Vector3(x, transform.localPosition.y, 0), position.x, _xMoveTime).SetEase(IconEase);
            var mY = DOTween.To(() => _startPosition.y, y => transform.localPosition = new Vector3(transform.localPosition.x, y,0), position.y, _yMoveTime).SetEase(IconEase);

            DOTween.Sequence().Insert(0, mX).Insert(1, mY);
        }

    }
}