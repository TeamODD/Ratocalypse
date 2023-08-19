using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CardLib.CommandLib
{
    public class TargetCommand<T> : Command where T : IDamageable
    {
        private T _target;

        public TargetCommand(T target)
        {
            _target = target;
        }

        protected ExecuteResult RunSuccess()
        {
            return new End(WrapResult(true));
        }

        protected ExecuteResult RunFail()
        {
            return new End(WrapResult(false));
        }

        protected Result WrapResult(bool success, ICommandResult innerResult = null)
        {
            return new Result(success, innerResult);
        }

        public override ExecuteResult Execute()
        {
            if(_target.IsAlive())
            {
                return RunSuccess();
            }
            else
            {
                return RunFail();
            }
        }

        public class Result : ICommandResult
        {
            public Result(bool success,ICommandResult innerResult = null)
            {
                Success = success;
                InnerResult = innerResult;
            }

            public bool Success = true;
            public ICommandResult InnerResult = null;
        }
    }
}