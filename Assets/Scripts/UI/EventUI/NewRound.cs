using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{

    public class NewRound : MonoBehaviour
    {
        private Color _fadeIn = Color.white;
        private Color _fadeOut = Color.clear;

        public float MoveTime;

        private bool _isEventAtivation = false;

        [ContextMenu("ExcuteEvent")]
        public void ExcuteEvent()
        {
            if (!_isEventAtivation) 
            { 
                transform.GetComponent<Image>().DOColor(_fadeOut, MoveTime).SetEase(Ease.InOutCubic); _isEventAtivation = true; 
            }
            else
            { 
                transform.GetComponent<Image>().DOColor(_fadeIn, MoveTime).SetEase(Ease.InOutCubic); _isEventAtivation = false; 
            }
        }

    }
}