using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;

namespace TeamOdd.Ratocalypse.DeckLib
{
    public class HandCard : MonoBehaviour
    {
        public enum CardState
        {
            Normal,
            Dragging,
            Focused,
            Unactive,
        }

        public enum CardAction
        {
            Focus,
            UnFocus,
            StartDrag,
            EndDrag,
            UpdateOrigin,
            Consume,
            None,
        }

        public CardState CurrentState { get; private set; } = CardState.Normal;
        private CardAction _currentAction = CardAction.None;

        private Vector3 _originPosition;
        private float _originZRotation;

        [SerializeField]
        private MovementValues _movementValues = new MovementValues();

        private Sequence _sequence = null;

        private Vector3 _cardTilt = Vector3.zero;
        private List<(Vector3 position, float time)> _prevPositions = new List<(Vector3, float)>();

        private CardGlow _cardGlow = null;

        public UnityEvent<HandCard> OnExecute { get; private set; } = new UnityEvent<HandCard>();


        private void Awake()
        {
            _cardGlow = GetComponent<CardGlow>();
            _sequence = DOTween.Sequence();
        }

        public void Initialize(MovementValues movementValues)
        {
            _currentAction = CardAction.None;
            CurrentState = CardState.Normal;
            _movementValues = movementValues;
            transform.localPosition = movementValues.drawPosition;
            OnExecute.RemoveAllListeners();
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
                CardAction.Consume => Consume,
                _ => null,
            };
        }

        public void Run(CardAction cardAction)
        {
            Action action = GetAction(cardAction);
            action?.Invoke();
        }

        private void Consume()
        {
            ResetSequence();
            _currentAction = CardAction.Consume;
            CurrentState = CardState.Unactive;
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
            if (AllowStates(CardState.Focused, CardState.Normal) || AllowActions(CardAction.Focus))
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

        public void ForceUpdateOrigin()
        {
            transform.localPosition = _originPosition;
            transform.localRotation = Quaternion.Euler(0f, 0f, _originZRotation);
            ResetSequence();
            CurrentState = CardState.Normal;
            _currentAction = CardAction.None;
        }

        private void UpdateOriginTransform()
        {
            if (AllowStates(CardState.Normal, CardState.Focused) || AllowActions(CardAction.UpdateOrigin))
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
            else if (AllowActions(CardAction.EndDrag, CardAction.UnFocus, CardAction.Focus))
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
                _cardGlow.SetActiveGlow();
                ResetSequence();

                Normal(() =>
                {
                    CurrentState = CardState.Normal;
                    _currentAction = CardAction.None;
                    RunEnd();
                }, _movementValues.moveSecond);

                
                if (transform.localPosition.y > _movementValues.ExecuteYlimit)
                {
                    OnExecute?.Invoke(this);
                }
            }
        }




        private void Normal(Action callback, float second)
        {
            _sequence.Join(TweenMove(_originPosition, second));
            Vector3 targetAngle = new Vector3(0f, 0f, _originZRotation);
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
            if (_prevPositions.Count < 2)
            {
                return Vector2.zero;
            }
            var (pa, ta) = _prevPositions[0];
            var (pb, tb) = _prevPositions[1];

            Vector2 velocity = (pb - pa) / (tb - ta);

            return velocity;
        }

        private bool _executeReady = false;

        private void Update()
        {
            if (AllowStates(CardState.Dragging))
            {
                Vector3 position = GetMousePosition(_movementValues.dragHeight);
                transform.position = position;

                if(transform.localPosition.y> _movementValues.ExecuteYlimit)
                {
                    if (!_executeReady)
                    {
                        transform.DOScale(_movementValues.ExecuteScale, _movementValues.ExecuteScaleTime).SetEase(Ease.OutBack);
                    }
                    _executeReady = true;
                    _cardGlow.SetHightLightGlow();
                }
                else
                {
                    if (_executeReady)
                    {
                        transform.DOScale(1, _movementValues.ExecuteScaleTime).SetEase(Ease.OutCubic);
                    }
                    _executeReady = false;
                    _cardGlow.SetActiveGlow();
                }


                UpadatePositions();
                Vector2 velocity = CalculateVelocity();
                Vector3 angle = new Vector3(velocity.y, -velocity.x, 0f);

                _cardTilt = Vector3.Lerp(_cardTilt, Vector3.zero, _movementValues.tiltReturnRatio);
                _cardTilt += angle * _movementValues.maxCardTiltSensitivity;
                var limit = _movementValues.maxCardTiltAngle;
                _cardTilt.x = Mathf.Clamp(_cardTilt.x, -limit, limit);
                _cardTilt.y = Mathf.Clamp(_cardTilt.y, -limit, limit);
                transform.localRotation = Quaternion.Euler(_cardTilt);
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

        public void Select(Selection<HandData> selection, Action cancel)
        {
            throw new NotImplementedException();
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

            public float ExecuteYlimit = 0.5f;
            public float ExecuteScale = 1.05f;
            public float ExecuteScaleTime = 0.5f;
        }

    }
}