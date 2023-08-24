using System;
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.CreatureLib.Attributes
{
    public interface IAnimatable
    {
        static public void PlayAnimation(IAnimatable target, object parm, string name, params Action[] callbacks) 
        {
            target.AnimationEvent.Invoke(parm, name, callbacks);
        } 
        public UnityEvent<object,string,Action[]> AnimationEvent { get; }
    }
}