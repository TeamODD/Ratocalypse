using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatIcon : MonoBehaviour, IIcon
{
    [field: SerializeField]
    public int Order { get; set; }

    public void SetPosition(Vector3 position)
    {
        StartCoroutine(MoveMent(position));
    }

    private IEnumerator MoveMent(Vector3 position)
    {
        while ((transform.localPosition - position).magnitude >= 0.001f)
        {
            GetComponent<Image>().transform.localPosition = Vector3.MoveTowards(transform.localPosition, position, (transform.localPosition - position).magnitude * Time.fixedDeltaTime);
            yield return null;
        }

        transform.localPosition = position;
    }
}
