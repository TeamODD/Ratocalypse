using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace TeamOdd.Ratocalypse.DeckLib
{
    public class CardEvents : MonoBehaviour
    {
        public UnityEvent MouseOverEvents = new UnityEvent();
        public UnityEvent MouseOutEvents = new UnityEvent();
        public UnityEvent MouseDownEvents = new UnityEvent();
        public UnityEvent MouseUpEvents = new UnityEvent();

        public void RemoveAllListeners()
        {
            MouseOverEvents.RemoveAllListeners();
            MouseOutEvents.RemoveAllListeners();
            MouseDownEvents.RemoveAllListeners();
            MouseUpEvents.RemoveAllListeners();
        }

        public void OnMouseOver()
        {
            MouseOverEvents.Invoke();
        }

        public void OnMouseExit()
        {
            MouseOutEvents.Invoke();
        }

        public void OnMouseDown()
        {
            MouseDownEvents.Invoke();
        }

        public void OnMouseUp()
        {
            MouseUpEvents.Invoke();
        }
    }
}