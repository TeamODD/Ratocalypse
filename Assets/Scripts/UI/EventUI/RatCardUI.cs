using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TeamOdd.Ratocalypse.CardLib;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatCardUI : MonoBehaviour
    {
        private Vector3 _offPosition = new Vector3(248, 0, 0);
        private Vector3 _onPosition = new Vector3(-248, 0, 0);
        private bool _cardChoice = false;

        public CardData CardData { get; set; }
        public Ease UIEase;
        public float MoveTime;

        public void EventExcute()
        { 
            if (_cardChoice == false) 
            {
                Debug.Log("카드 데이터 숫자, 이미지 적용");
                DOTween.To(() => _offPosition, x => transform.localPosition = x, _onPosition, MoveTime).SetEase(UIEase);
                _cardChoice = true;
            } else 
            {
                DOTween.To(() => _onPosition, x => transform.localPosition = x, _offPosition, MoveTime).SetEase(UIEase);
                Debug.Log("카드 반환");
                _cardChoice = false;
            }
        }


        public void ActivationChange()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
