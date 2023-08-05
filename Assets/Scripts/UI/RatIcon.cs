using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RatIcon : MonoBehaviour ,IIcon
{
    private Vector3 _startpoint;

    [field: SerializeField]
    public int Order { get; set; }
    public Ease IconEase;
    public float _moveTime = 2f;

    public void SetPosition(Vector3 position)
    {
        _startpoint = transform.localPosition;
        DOTween.To(() => _startpoint, x => transform.localPosition = x, position, _moveTime).SetEase(IconEase);
    }

}
