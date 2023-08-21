using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class CreatureUI : MonoBehaviour
{
    // 테스트용 임시 변수
    public Transform target;
    public RectTransform rectTransform;

    private Vector3 InterfacePoint;  
    public void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }
    public void LateUpdate()
    {
        // transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        //transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, 150, 0)) ;
         InterfacePoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        rectTransform.anchoredPosition = InterfacePoint + new Vector3(0, 150, 0);
    }
}
