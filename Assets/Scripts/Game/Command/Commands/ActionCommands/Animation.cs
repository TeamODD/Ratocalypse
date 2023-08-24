using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using System;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands
{
    public class Animation : Command
    {
        private IAnimatable _target;
        private string _animationName;
        private object _parm;
        private Action[] _callbacks;
        private bool _wait;

        public Animation(object target, string animationName, bool wait, object parm, params Action[] callbacks)
        {
            if(target is IAnimatable animateTarget)
            {
                _target = animateTarget;
                _animationName = animationName;
                _parm = parm;
                _callbacks = callbacks;
                _wait = wait;
            }
        }

        public override ExecuteResult Execute()
        {
            if(_target == null)
            {
                for(int i = 0; i < _callbacks.Length; i++)
                {
                    _callbacks[i]?.Invoke();
                }
                return new End();
            }

            if (_wait)
            {
                var (endWait, result) = CreateWait();
                
                var last = _callbacks.Last();
                _callbacks[^1] = () =>
                {
                    last();
                    endWait(new End());
                };
                IAnimatable.PlayAnimation(_target, _parm, _animationName, _callbacks);
                return result;
            }
            else
            {
                IAnimatable.PlayAnimation(_target, _parm, _animationName, () => { });
                return new End();
            }
            
        }
    }
}