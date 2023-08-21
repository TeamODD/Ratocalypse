using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{

    public class NewRound : MonoBehaviour
    {
        private Color _start = Color.white;
        private Color _end = Color.clear;

        public float MoveTime;

        [ContextMenu("FadeOut")]
        public void FadeOut()
        {
            transform.GetComponent<Image>().DOColor(_end, MoveTime).SetEase(Ease.InOutCubic);
        }
        [ContextMenu("FadeIn")]
        public void FadeIn()
        {
            transform.GetComponent<Image>().DOColor(_start, MoveTime).SetEase(Ease.InOutCubic);
        }

        public void ActivationChange()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

    }
}