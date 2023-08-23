using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using Unity.VisualScripting;

namespace TeamOdd.Ratocalypse.UI
{
    public class DefeatEffect : MonoBehaviour
    {
        private Color _fadeIn = Color.white;
        private Color _fadeOut = Color.clear;
        public float MoveTime;

        [SerializeField]
        private GameObject _panel;
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