using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class Command
    {
        protected UnityEvent<ICommandResult> _onEnd = new UnityEvent<ICommandResult>();

        public virtual ExecuteResult Execute()
        {
            return new End(null);
        }

        public void RegisterOnEnd(UnityAction<ICommandResult> action)
        {
            _onEnd.AddListener(action);
        }

        protected (Action<ExecuteResult> end, Wait result) CreateWait()
        {
            UnityEvent<ExecuteResult> callback = new UnityEvent<ExecuteResult>();
            void endWait(ExecuteResult result)
            {   
                callback?.Invoke(result);
                callback.RemoveAllListeners();
            }
            return (endWait,new Wait(callback));
        }

        public void End(ICommandResult result)
        {
            _onEnd?.Invoke(result);
            _onEnd.RemoveAllListeners();
        }
    }
}