using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatEvent : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offPosition = new Vector3(248, 0, 0);

        [SerializeField]
        private Vector3 _onPosition = new Vector3(-248, 0, 0);

        [SerializeField]
        private Ease _ease = Ease.InOutCubic;

        [SerializeField]
        private TextMeshProUGUI _cardTitle;

        [SerializeField]
        private Image _cardCardillustration;

        [SerializeField]
        private Image _mold;

        [SerializeField]
        private TextMeshProUGUI _cardExplanation;

        [SerializeField]
        private Image _chessPiecesIcon;

        [SerializeField]
        private Image _backGround;

        public float MoveTime;

        private bool _isEventAtivation = false;

        [ContextMenu("ExecuteEvent")]
        public void ExecuteEvent()
        {
            if (!_isEventAtivation)
            { 
                DOTween.To(() => _offPosition, x => transform.localPosition = x, _onPosition, MoveTime).SetEase(_ease); _isEventAtivation = true;
            }
            else 
            {
                DOTween.To(() => _onPosition, x => transform.localPosition = x, _offPosition, MoveTime).SetEase(_ease); _isEventAtivation = false; 
            }   
        }

        public void ActivationChange()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
