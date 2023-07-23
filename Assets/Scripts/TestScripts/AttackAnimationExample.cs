using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using System;

namespace TeamOdd.Ratocalypse.TestScripts
{
    public class AttackAnimationExample : MonoBehaviour
    {
        [Header ("Up")]
        public float Height = 1;
        public float UpDuration = 1;
        public Ease UpEase = Ease.OutCubic;
        public float UpTime = 0;
        [Header ("Down")]
        public float DownDuration = 1;
        public Ease DownEase = Ease.InCubic;
        public float DownTime = 0;
        [Header ("FirstRotate")]
        public float FirstRotateDuration = 1;
        public Ease FirstRotateEase = Ease.InQuart;
        public float FirstRotateTime = 0;
        [Header ("SecondRotate")]
        public float SecondRotateDuration = 1;
        public Ease SecondRotateEase = Ease.InQuart;
        public float SecondRotateTime = 0;

        
        public void Jump(Action firstRotationMiddleCallback, Action secondRotationMiddleCallback, Action endCallback)
        {
            Vector3 angle = new Vector3(0,720,0);
            var rotate1 = transform.DORotate(angle, FirstRotateDuration,RotateMode.FastBeyond360).SetEase(FirstRotateEase);
            var rotate2 = transform.DORotate(angle, SecondRotateDuration,RotateMode.FastBeyond360).SetEase(SecondRotateEase);
            var up = transform.DOMoveY(transform.position.y + Height, UpDuration).SetEase(UpEase);
            var down = transform.DOMoveY(transform.position.y, DownDuration).SetEase(DownEase);
            DOTween.Sequence().Insert(UpTime,up)
            .Insert(DownTime,down)
            .Insert(FirstRotateTime,rotate1)
            .Insert(SecondRotateTime,rotate2)
            .InsertCallback(FirstRotateTime + (FirstRotateDuration / 2), ()=>{firstRotationMiddleCallback?.Invoke();})
            .InsertCallback(SecondRotateTime + (SecondRotateDuration / 2), ()=>{secondRotationMiddleCallback?.Invoke();})
            .OnComplete(()=>{endCallback?.Invoke();});
        }
    }
}