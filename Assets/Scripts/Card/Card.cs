using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class Card : MonoBehaviour
    {
        public UnityEvent MouseOverEvents;
        public UnityEvent MouseOutEvents;
        public UnityEvent MouseDragEvents;
        public UnityEvent MouseUpEvents;

        private CardData _cardData;

        public void Awake()
        {
            
        }

        public void Initialize(CardData cardData)
        {
            _cardData = cardData;
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

        public void OnMouseUp()
        {
            MouseUpEvents.Invoke();
        }

        public void SetPosition(float x, float y)
        {
            SetPosition(new Vector3(x, y));
        }

        public void SetZRotation(float z)
        {
            var rotation = transform.rotation;
            rotation.z = z;
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

        public void DefaultDrag()
        {
            Vector3 position = GetMousePosition(transform);
            SetPosition(position);
        }

        private static Vector3 GetMousePosition(Transform transform)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new NullReferenceException("Camera.main");
            }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
            Vector3 position = ray.GetPoint(distance);
            position.z = 0.0f;
            return position;
        }

    }
}