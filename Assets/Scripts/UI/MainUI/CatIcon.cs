using UnityEngine;
using System.Collections;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Cat;

namespace TeamOdd.Ratocalypse.UI
{
    public class CatIcon : MonoBehaviour, IIcon
    {
        private Vector3 _startPosition;

        public int Order { get => -_catData.Stamina; }
        public Ease IconEase;
        public float _moveTime = 2f;
        
        private CatData _catData;
        
        public void SetPosition(Vector3 position)
        {
            _startPosition = transform.localPosition;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);

        }

        public void Initialize(CatData catData)
        {
            _catData = catData;
        }


    }
}