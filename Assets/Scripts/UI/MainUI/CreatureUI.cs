using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CreatureUI : MonoBehaviour
{
    // 테스트용 임시 변수
    public Transform target;

    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position) + new Vector3(0, 150, 0);
    }
}
