using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class HPTest : MonoBehaviour
{
    public Transform targetTr;
    public Vector3 offset;
    public Transform rectHp;
    public Camera uiCamera;
    public RectTransform rectParent;
    public void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        //카메라의 뒷쪽 영역(180도 회전)일 때 좌푯값 보정
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }
        //RectTransform 좌푯값을 전달받을 변수
        var localPos = Vector2.zero;
        //스크린 좌표를 RectTransform 기준의 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        //생명 게이지 이미지의 위치를 변경
        rectHp.localPosition = localPos;
    }
}
