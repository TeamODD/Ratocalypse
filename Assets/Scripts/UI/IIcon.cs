using UnityEngine;


public interface IIcon 
{
    [SerializeField]
    public int Order {get; set; }
    [SerializeField]
    public Vector3 SetPosition { get; set; }

}
