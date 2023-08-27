using UnityEngine;

public class ShowStatusDetail : MonoBehaviour
{
    [SerializeField]
    private GameObject _statusDetailPrefab;
    private GameObject _statusDetailUi;

    // private void OnMouseEnter()
    // {
    //     Destroy(_statusDetailUi);
    //     //Debug.Log("enter");
    //     _statusDetailUi = Instantiate(_statusDetailPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    //     _statusDetailUi.GetComponent<Transform>().SetParent(transform.parent.parent, false);
    //     _statusDetailUi.transform.localPosition = transform.localPosition + transform.parent.localPosition;
    // }

    // private void OnMouseOver()
    // {
    //     //Debug.Log("over");
    //     _statusDetailUi.GetComponent<RectTransform>().anchoredPosition3D = 
    //         Input.mousePosition - _statusDetailUi.transform.parent.GetComponent<RectTransform>().anchoredPosition3D;
    // }

    // private void OnMouseExit()
    // {
    //     //Debug.Log("end");
    //     Destroy(_statusDetailUi);
    // }


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
