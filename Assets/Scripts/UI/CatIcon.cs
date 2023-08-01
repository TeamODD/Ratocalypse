using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatIcon : MonoBehaviour, Icon
{
    [field: SerializeField] 
    public int Order { get; set; }
    [field: SerializeField]
    public Vector3 SetPosition { get; set; }
  
    // Update is called once per frame
    private void Update()
    {
        GetComponent<Image>().rectTransform.localPosition = Vector3.MoveTowards(transform.localPosition, SetPosition, (SetPosition - transform.position).magnitude * Time.deltaTime );
    }
}
