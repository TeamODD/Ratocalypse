using UnityEngine;


public interface Icon 
{
    [SerializeField]
    public int Order {get; set; }
    [SerializeField]
    public Vector3 SetPosition { get; set; }

}
