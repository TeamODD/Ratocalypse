using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public class CreatureUI : MonoBehaviour
    {
        // 테스트용 임시 변수
        public Transform target;

        private RectTransform _rectTransform;
        private Vector3 _InterfacePoint;

        public void Start()
        {
            _rectTransform = transform.GetComponent<RectTransform>();
        }
        public void LateUpdate()
        {
            // transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
            //transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, 150, 0)) ;
            _InterfacePoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
            _rectTransform.anchoredPosition = _InterfacePoint + new Vector3(0, 150, 0);
        }
    }
}
