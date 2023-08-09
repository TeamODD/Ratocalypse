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

        protected (Action<ICommandResult> end, Wait result) CreateWait()
        {
            UnityEvent callback = new UnityEvent();
            void endWait(ICommandResult result)
            {
                _onEnd?.Invoke(result);
                _onEnd.RemoveAllListeners();
                
                callback?.Invoke();
                callback.RemoveAllListeners();
            }
            return (endWait,new Wait(callback));
        }

        protected void End(ICommandResult result)
        {
            _onEnd?.Invoke(result);
            _onEnd.RemoveAllListeners();
        }

        protected End EndCommand(ICommandResult result)
        {
            End(result);
            return new End(result);
        }

        protected NextCommand NextCommand(Command next, ICommandResult result = null)
        {
            End(result);
            return new NextCommand(next);
        }

        protected NextCommands NextCommands(List<Command> nexts, ICommandResult result = null)
        {
            End(result);
            return new NextCommands(nexts);
        }


    }
}