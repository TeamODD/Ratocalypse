using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{

    public class NewRound : MonoBehaviour
    {
        private Vector3 _startPosition = new Vector3 (0,720, 0);
        private Vector3 _endPosition = new Vector3(0, 0, 0);
        private Ease _ease = Ease.OutBounce;

        public float MoveTime;
        public Sprite test;
        public void Ation()
        {
            GetComponent<Image>().sprite = test;
            DOTween.To(() => _startPosition, x => transform.localPosition = x, _endPosition, MoveTime).SetEase(_ease);
        }


    }
}