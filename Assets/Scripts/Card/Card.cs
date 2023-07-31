using System;
using Unity.VisualScripting;
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

        private const float TestTimeStart = 0;
        private const float TestTimeEnd = 1;

        private static readonly Vector3 TestLeftTop = new Vector3(-12, 4);
        private static readonly Vector3 TestRightBottom = new Vector3(-10, 2);
        private static readonly Vector3 TestMiddle = new Vector3((TestLeftTop.x + TestRightBottom.x) / 2, (TestLeftTop.y + TestRightBottom.y) / 2);

        public UnityEvent MouseOverEvents;
        public UnityEvent MouseOutEvents;
        public UnityEvent MouseDragEvents;
        public UnityEvent MouseUpEvents;

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

        public void OnMouseUp()
        {
            MouseUpEvents.Invoke();
        }

        public void SetPosition(float x, float y)
        {
            SetPosition(new Vector3(x, y));
        }

        public void SetPosition(Vector3 vector)
        {
            transform.position = vector;
        }

        public void SetPositionWithAnimation(float x, float y, float timeStart, float timeEnd)
        {
            SetPositionWithAnimation(new Vector3(x, y), timeStart, timeEnd);
        }

        public void SetPositionWithAnimation(Vector3 vector, float timeStart, float timeEnd)
        {
            Animation animation = GetComponent<Animation>();
            if (!animation)
            {
                animation = this.AddComponent<Animation>();
            }
            Vector3 currentPosition = transform.position;
            AnimationClip clip = new AnimationClip();
            AnimationCurve curveX = AnimationCurve.Linear(timeStart, currentPosition.x, timeEnd, vector.x);
            AnimationCurve curveY = AnimationCurve.Linear(timeStart, currentPosition.y, timeEnd, vector.y);
            clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
            clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
            clip.name = "Move";
            clip.legacy = true;
            animation.clip = clip;
            animation.AddClip(clip, clip.name);
            animation.Play();
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
            Vector3 position = GetMousePosition(transform);
            SetPosition(position);
        }

        public void AttachCardCastArea()
        {
            Vector3 position = GetMousePosition(transform);
            if (HasPointEnteredArea(position, TestLeftTop, TestRightBottom))
            {
                SetPosition(TestMiddle);
            }
        }

        public void ExecuteCommand()
        {
            Vector3 position = GetMousePosition(transform);
            if (HasPointEnteredArea(position, TestLeftTop, TestRightBottom))
            {
                Debug.Log("Card is cast!");
                return;
            }
            SetPositionWithAnimation(DefaultPositionX, DefaultPositionY, TestTimeStart, TestTimeEnd);
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

        private static bool HasPointEnteredArea(Vector3 point, Vector3 leftTop, Vector3 rightBottom)
        {
            bool checkX = leftTop.x <= point.x && point.x <= rightBottom.x;
            if (checkX)
            {
                return rightBottom.y <= point.y && point.y <= leftTop.y;
            }
            return false;
        }
    }
}