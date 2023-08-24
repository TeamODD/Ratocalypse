using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public abstract record ExecuteResult()
    {
        public record SubCommand(Command Command) : ExecuteResult;
        public record NextCommand(Command Command, ICommandResult Result = null) : ExecuteResult;
        public record NextCommands(List<Command> Commands, ICommandResult Result = null) : ExecuteResult;
        public record End(ICommandResult Result = null) : ExecuteResult;
        public record Wait(UnityEvent<ExecuteResult> Callback) : ExecuteResult;
    }

}