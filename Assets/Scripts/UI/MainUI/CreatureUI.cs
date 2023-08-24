using UnityEngine;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] // 테스트용 SerializeField
    private Transform _target;
    [SerializeField]
    private int _uiOffset = 150;
    private RectTransform _rectTransform;
    private Vector3 _interfacePoint;  
    
    public void AttachUi(Transform target, int offset)
    {
        _target = target;
        _uiOffset = offset;
    }
    private void Start()
    {
        _rectTransform = transform.GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        // transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        //transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, 150, 0)) ;
        _interfacePoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.position);
        _rectTransform.anchoredPosition = _interfacePoint + new Vector3(0, _uiOffset, 0);
    }
}
