using UnityEngine;
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.Card
{
    public class Card : MonoBehaviour
    {
        private const float DefaultPositionX = 0;
        private const float DefaultPositionY = 0;

        private const float DefaultX = 2.0f;
        private const float DefaultY = 2.88f;
        private const float DefaultZ = 0.05f;

        public UnityEvent MouseOverEvents;
        public UnityEvent MouseOutEvents;
        public UnityEvent MouseDragEvents;

        protected CardDataValue _cardDataValue;

        public void Awake()
        {
            SetPosition(DefaultPositionX, DefaultPositionY);
            SetScale(DefaultX, DefaultY, DefaultZ);
        }

        public void Start()
        {
            // TODO: implement this...
        }

        public void Update()
        {
            // TODO: implement this...
        }

        public void AttachDataValue(CardDataValue value)
        {
            _cardDataValue = value;
        }

        public void OnMouseOver()
        {
            MouseOverEvents.Invoke();
        }

        public void OnMouseExit()
        {
            MouseOutEvents.Invoke();
        }

        public void OnMouseDrag()
        {
            MouseDragEvents.Invoke();
        }

        public void SetPosition(float x, float y)
        {
            SetPosition(new Vector3(x, y));
        }

        public void SetPosition(Vector3 vector)
        {
            transform.position = vector;
        }

        public void SetScale(float x, float y, float z)
        {
            SetScale(new Vector3(x, y, z));
        }

        public void SetScale(Vector3 vector)
        {
            transform.localScale = vector;
        }

        public void Initiate(CardData cardData, bool clone = false)
        {
            Initiate(clone ? cardData.DataValue.Clone() : cardData.DataValue);
        }

        public void Initiate(CardDataValue cardDataValue)
        {
            _cardDataValue = cardDataValue;
        }

        public void DefaultMouseOver()
        {
            const float scale = 1.25f;
            SetScale(DefaultX * scale, DefaultY * scale, DefaultZ);
        }

        public void DefaultMouseOut()
        {
            SetScale(DefaultX, DefaultY, DefaultZ);
        }

        public void DefaultDrag()
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                return;
            }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
            Vector3 position = ray.GetPoint(distance);
            position.z = 0.0f;
            SetPosition(position);
        }
    }
}