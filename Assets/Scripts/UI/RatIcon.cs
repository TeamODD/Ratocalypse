using UnityEngine;
using UnityEngine.UI;
public class RatIcon : MonoBehaviour ,Icon
{
    [field: SerializeField]
    public int Order { get; set; }
    [field: SerializeField]
    public Vector3 SetPosition { get; set; }
   
    // Update is called once per frame
    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, SetPosition, (SetPosition - transform.position).magnitude * Time.deltaTime*2 );
    }
}
