using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace TeamOdd.Ratocalypse.DeckLib
{
    public class HandCard : MonoBehaviour
    {
        public enum CardState
        {
            Normal,
            Dragging,
            Focused,
        }

        public enum CardAction
        {
            Focus,
            UnFocus,
            StartDrag,
            EndDrag,
            UpdateOrigin,
            None,
        }

        [field:ReadOnly, SerializeField]
        public CardState CurrentState { get; private set; } = CardState.Normal;
        [ReadOnly, SerializeField]
        private CardAction _currentAction = CardAction.None;

        private Vector3 _originPosition;
        private float _originZRotation;

        [SerializeField]
        private MovementValues _movementValues = new MovementValues();

        private Sequence _sequence = null;

        private Vector3 _cardTilt = Vector3.zero;
        private List<(Vector3 position,float time)> _prevPositions = new List<(Vector3,float)>();




        private void Awake()
        {
            _sequence = DOTween.Sequence();
        }


        public void Initialize(MovementValues movementValues)
        {
            _movementValues = movementValues;
            transform.localPosition = movementValues.drawPosition;
        }

        private Action GetAction(CardAction action)
        {
            return action switch
            {
                CardAction.Focus => Focus,
                CardAction.UnFocus => UnFocus,
                CardAction.StartDrag => StartDrag,
                CardAction.EndDrag => EndDrag,
                CardAction.UpdateOrigin => UpdateOriginTransform,
                _ => null,
            };
        }

        public void Run(CardAction cardAction)
        {
            Action action = GetAction(cardAction);
            action?.Invoke();
        }

        private void Focus()
        {
            if (AllowStates(CardState.Normal) || AllowActions(CardAction.UnFocus))
            {
                _currentAction = CardAction.Focus;
                ResetSequence();
                FocusedPosition(() =>
                {
                    CurrentState = CardState.Focused;
                    RunEnd();
                }, _movementValues.focusSecond);
            }
        }

        private void UnFocus()
        {
            if (AllowStates(CardState.Focused,CardState.Normal) || AllowActions(CardAction.Focus))
            {
                _currentAction = CardAction.UnFocus;
                ResetSequence();
                Normal(() =>
                {
                    CurrentState = CardState.Normal;
                    RunEnd();
                }, _movementValues.focusSecond);
            }
        }

        public void SetOrigin(Vector3 originPosition, float originZRotation)
        {
            _originPosition = originPosition;
            _originZRotation = originZRotation;
        }

        private void UpdateOriginTransform()
        {
            if (AllowStates(CardState.Normal, CardState.Focused)||AllowActions(CardAction.UpdateOrigin))     
            {   
                ResetSequence();
                _currentAction = CardAction.UpdateOrigin;

                float second = _movementValues.moveSecond;
                if (CurrentState == CardState.Focused)
                {
                    FocusedPosition(() =>
                    {
                        CurrentState = CardState.Focused;
                        RunEnd();
                    }, second);
                }
                else if (CurrentState == CardState.Normal)
                {
                    Normal(() =>
                    {
                        CurrentState = CardState.Normal;
                        RunEnd();
                    }, second);
                }
            }
            else if(AllowActions(CardAction.EndDrag, CardAction.UnFocus, CardAction.Focus))
            {
                CardAction action = _currentAction;
                _currentAction = CardAction.None;
                Run(action);
            }
        }

        private void RunEnd()
        {
            _currentAction = CardAction.None;
        }

        private void StartDrag()
        {
            if (AllowStates(CardState.Focused) || AllowActions(CardAction.Focus))
            {
                CurrentState = CardState.Dragging;
                _prevPositions.Clear();
                ResetSequence();
                TweenScale(Vector3.one, _movementValues.focusSecond);
                TweenRotation(Vector3.zero, _movementValues.focusSecond);
                RunEnd();
            }
        }

        private void EndDrag()
        {
            if (AllowStates(CardState.Dragging))
            {
                _currentAction = CardAction.EndDrag;
                ResetSequence();
                Normal(() =>
                {
                    CurrentState = CardState.Normal;
                    _currentAction = CardAction.None;
                    RunEnd();
                }, _movementValues.moveSecond);
            }
        }




        private void Normal(Action callback, float second)
        {
            _sequence.Join(TweenMove(_originPosition, second));
            Vector3 targetAngle = new Vector3(0f, 0f ,_originZRotation);
            _sequence.Join(TweenRotation(targetAngle, second));
            _sequence.Join(TweenScale(new Vector3(1f, 1f, 1f), second));

            _sequence.AppendCallback(() => { callback(); });
        }

        private void FocusedPosition(Action callback, float second)
        {
            _currentAction = CardAction.Focus;

            Vector3 destination = _originPosition;
            destination.y = _movementValues.focusY;
            destination.z -= _movementValues.focusHeight;

            _sequence.Join(TweenMove(destination, second));
            _sequence.Join(TweenRotation(Vector3.zero, second));
            _sequence.Join(TweenScale(_movementValues.focusScale, second));

            _sequence.AppendCallback(() => { callback(); });
        }

        private Tween TweenScale(Vector3 scale, float second)
        {
            Tween scaling = transform.DOScale(scale, second);
            scaling.SetEase(Ease.InOutCubic);
            return scaling;
        }

        private Tween TweenMove(Vector3 position, float second)
        {
            Tween movement = transform.DOLocalMove(position, second);
            movement.SetEase(Ease.InOutCubic);
            return movement;
        }

        private Tween TweenRotation(Vector3 angle, float second)
        {
            Tween rotation = transform.DOLocalRotate(angle, second);
            rotation.SetEase(Ease.InOutCubic);
            return rotation;
        }


        private Vector3 GetMousePosition(float height)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                throw new NullReferenceException("Camera.main");
            }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance = transform.parent.localPosition.z - height;
            Vector3 position = ray.GetPoint(distance);
            return position;
        }
        

        private void UpadatePositions()
        {
            if (_prevPositions.Count >= 2)
            {
                _prevPositions.RemoveAt(0);
            }
            _prevPositions.Add((transform.position, Time.time));
        }

        private Vector2 CalculateVelocity()
        {
            if(_prevPositions.Count < 2)
            {
                return Vector2.zero;
            }
            var (pa,ta) = _prevPositions[0];
            var (pb,tb) = _prevPositions[1];

            Vector2 velocity = (pb-pa)/(tb-ta);

            return velocity;
        }

        private void Update()
        {
            if (AllowStates(CardState.Dragging))
            {
                Vector3 position = GetMousePosition(_movementValues.dragHeight);
                transform.position = position;
                

                UpadatePositions();
                Vector2 velocity = CalculateVelocity();
                Vector3 angle = new Vector3(velocity.y, -velocity.x, 0f);

                _cardTilt = Vector3.Lerp(_cardTilt, Vector3.zero, _movementValues.tiltReturnRatio);
                _cardTilt += angle*_movementValues.maxCardTiltSensitivity;
                var limit = _movementValues.maxCardTiltAngle;
                _cardTilt.x = Mathf.Clamp(_cardTilt.x, -limit, limit);
                _cardTilt.y = Mathf.Clamp(_cardTilt.y, -limit, limit);
                transform.localRotation  = Quaternion.Euler(_cardTilt);
            }
        }

        private bool AllowStates(params CardState[] states)
        {
            if (_currentAction != CardAction.None)
            {
                return false;
            }
            return states.Contains(CurrentState);
        }

        private bool AllowActions(params CardAction[] actions)
        {
            return actions.Contains(_currentAction);
        }

        private void ResetSequence()
        {
            _sequence.Kill();
            _sequence.Kill(true);
            _sequence = DOTween.Sequence();
        }

        [System.Serializable]
        public class MovementValues
        {
            public float focusSecond = 0.5f;
            public float moveSecond = 0.5f;
            public float dragHeight = 1f;
            public float focusHeight = 0.8f;
            public float focusY = 0f;
            public Vector3 focusScale = new Vector3(1.5f, 1.5f, 1.5f);
            public Vector3 drawPosition = new Vector3(0f, 0f, 0f);
            public Vector3 TombPosition = new Vector3(0f, 0f, 0f);
            public float tiltReturnRatio = 0.1f;
            public float maxCardTiltAngle = 10f;
            public float maxCardTiltSensitivity = 0.5f;
        }

    }
}