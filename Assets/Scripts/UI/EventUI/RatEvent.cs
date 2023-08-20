using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TeamOdd.Ratocalypse.CardLib;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatEvent : MonoBehaviour
    {
        private Vector3 _offPosition = new Vector3(248, 0, 0);
        private Vector3 _onPosition = new Vector3(-248, 0, 0);

        public CardData CardData { get; set; }
        private Ease _ease = Ease.InOutCubic;
        public float MoveTime;

        [ContextMenu("EventExcute")]
        public void EventExcute()
        {
            Debug.Log("카드 데이터 숫자, 이미지 적용");
            DOTween.To(() => _offPosition, x => transform.localPosition = x, _onPosition, MoveTime).SetEase(_ease);
        }
        public void CancelButton()
        {
            DOTween.To(() => _onPosition, x => transform.localPosition = x, _offPosition, MoveTime).SetEase(_ease);
            Debug.Log("카드 반환");
        }

        public void CardAction() 
        {
        
        }
        public void ActivationChange()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
