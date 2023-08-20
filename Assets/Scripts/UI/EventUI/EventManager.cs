using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace TeamOdd.Ratocalypse.UI
{
    public class EventManager : MonoBehaviour
    {

        public UnityEvent NewRound;

        [ContextMenu("EventAtion")]
        public void EventAtion() { NewRound.Invoke(); }

    }
}
