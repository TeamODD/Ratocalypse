using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.UI
{
    public class VictoryEffect : MonoBehaviour
    {
        private Color _fadeIn = Color.white;
        private Color _fadeOut = Color.clear;
        public float MoveTime;

        [SerializeField]
        private GameObject _panel;

        [ContextMenu("FadeOut")]
        public void FadeOut()
        {
            _panel.GetComponent<Image>().DOColor(new Color(0.1f, 0.1f, 0.1f, 0), MoveTime).SetEase(Ease.InOutCubic);
            transform.GetComponent<Image>().DOColor(_fadeOut, MoveTime).SetEase(Ease.InOutCubic);
            
        }
        [ContextMenu("FadeIn")]
        public void FadeIn()
        {
            _panel.GetComponent<Image>().DOColor(new Color(0.1f, 0.1f, 0.1f, 0.8f), MoveTime).SetEase(Ease.InOutCubic);
            transform.GetComponent<Image>().DOColor(_fadeIn, MoveTime).SetEase(Ease.InOutCubic);
        }
    }
}