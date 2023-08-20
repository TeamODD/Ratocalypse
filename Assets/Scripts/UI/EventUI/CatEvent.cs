using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;


namespace TeamOdd.Ratocalypse.UI
{
    public class CatEvent : MonoBehaviour
    {
        private Vector3 _onPosition = new Vector3(248, 0, 0);
        private Vector3 _offPosition = new Vector3(-248, 0, 0);
        private bool _cardChoice = false;
        private Ease _ease = Ease.InOutCubic;

        public CardData CardData { get; set; }
       
        public float MoveTime;

        [ContextMenu("EventExcute")]
        public void EventExcute()
        {
            if (_cardChoice == false)
            {
                Debug.Log("카드 데이터 숫자, 이미지 적용");
                DOTween.To(() => _offPosition, x => transform.localPosition = x, _onPosition, MoveTime).SetEase(_ease);
                _cardChoice = true;
            }
            else
            {
                DOTween.To(() => _onPosition, x => transform.localPosition = x, _offPosition, MoveTime).SetEase(_ease);
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
