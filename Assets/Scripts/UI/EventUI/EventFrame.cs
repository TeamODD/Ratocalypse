using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventFrame : MonoBehaviour
{
    [SerializeField]
    private Image[] _frameImage= new Image[4];
    
    private Color _eventColor = Color.red;
    private Color _endColor = Color.clear;
    [SerializeField]
    private Color _currentColor = Color.clear;

    [field: SerializeField]
    public bool Ativation { get; set; }

    [SerializeField]
    private Ease _ease = Ease.InOutCubic;

    private IEnumerator Animation;

    public float MoveTime;

    public void Start()
    {
        for (int i =0;i<transform.childCount;i++) 
        {
            _frameImage[i] = transform.GetChild(i).GetComponent<Image>();
        }
        Animation = Shine();
    }
    [ContextMenu("ExcuteEvent")]
    public void ExcuteEvent()
    {
        Ativation = Ativation ?false:true;

        StartCoroutine(Animation);
    }
    private IEnumerator Shine()
    {
        while (true)
        {
            if (Ativation == true) {
                if (_currentColor == _eventColor)
                {
                    DOTween.To(() => _eventColor, x => _currentColor = x, _endColor, MoveTime);
                }
                else if (_currentColor == _endColor)
                {
                    DOTween.To(() => _endColor, x => _currentColor = x, _eventColor, MoveTime);
                }
            }
            else
            {
                _currentColor = _endColor;
                break;
            }

            for (int i = 0; i < _frameImage.Length; i++)
            {
                _frameImage[i].color = _currentColor;
                yield return null;
            }


        }

    }


}
