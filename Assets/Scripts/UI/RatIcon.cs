using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RatIcon : MonoBehaviour ,IIcon
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
            GetComponent<Image>().transform.localPosition = Vector3.MoveTowards(transform.localPosition, position, 2* Time.fixedDeltaTime);
           yield return null;
        }

        transform.localPosition= position;
    }
}
