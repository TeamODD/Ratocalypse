using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using Unity.VisualScripting;
using System;

namespace TeamOdd.Ratocalypse.UI
{
    public class PlayEvent : MonoBehaviour
    {
        public float MoveTime;
        [SerializeField]
        private List<RawImage> _rawImages = new List<RawImage>(); 
        [SerializeField]
        private List<Ease> _eases = new List<Ease>();
        [SerializeField]
        private RawImage _background;

        public void PlayLoop(int number,Action callback)
        {
            _rawImages[number].DOFade(1, MoveTime).SetEase(_eases[number]).OnComplete(() =>
            {
                _rawImages[number].DOFade(0, MoveTime).SetEase(_eases[number]).OnComplete(() =>
                {
                    callback();
                });
            });
        }

        public void Show(int number, Action callback)
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, _rawImages[number].transform.DOScale(1, MoveTime));
            sequence.Insert(0, _background.DOFade(0.5f, MoveTime).SetEase(_eases[number]));

            sequence.Insert(0, _rawImages[number].DOFade(1, MoveTime));
            sequence.InsertCallback(5, () =>
            {
                callback();
            });
        }

    }
}