using UnityEngine;


public interface IIcon 
{
    [SerializeField]
    public int Order {get; set; }
    [SerializeField]
    public void SetPosition(Vector3 position);

}
