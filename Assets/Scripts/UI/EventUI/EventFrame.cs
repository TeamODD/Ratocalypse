using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{
    public class EventFrame : MonoBehaviour
    {
        [SerializeField]
        private Image[] _frameImage = new Image[4];

        [field : SerializeField]
        public Color EventColor { get; private set; }

        private Color _fadeOutColor = Color.clear;

        [field: SerializeField]
        public bool Ativation { get; private set; }

        [SerializeField]
        private Ease _ease = Ease.InOutCubic;

        public float MoveTime;

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _frameImage[i] = transform.GetChild(i).GetComponent<Image>();
            }

        }

        [ContextMenu("ExcuteEvent")]
        public void ExcuteEvent()
        {
            if (Ativation)
            {
                DOTween.To(() => _fadeOutColor, x => _frameImage[0].color = x, EventColor, MoveTime).SetEase(_ease);
                DOTween.To(() => _fadeOutColor, x => _frameImage[1].color = x, EventColor, MoveTime).SetEase(_ease);
                DOTween.To(() => _fadeOutColor, x => _frameImage[2].color = x, EventColor, MoveTime).SetEase(_ease);
                DOTween.To(() => _fadeOutColor, x => _frameImage[3].color = x, EventColor, MoveTime).SetEase(_ease);

                Ativation = false;
            }
            else
            {
                DOTween.To(() => EventColor, x => _frameImage[0].color = x, _fadeOutColor, MoveTime).SetEase(_ease);
                DOTween.To(() => EventColor, x => _frameImage[1].color = x, _fadeOutColor, MoveTime).SetEase(_ease);
                DOTween.To(() => EventColor, x => _frameImage[2].color = x, _fadeOutColor, MoveTime).SetEase(_ease);
                DOTween.To(() => EventColor, x => _frameImage[3].color = x, _fadeOutColor, MoveTime).SetEase(_ease);

                Ativation = true;
            }
        }
    }
}
