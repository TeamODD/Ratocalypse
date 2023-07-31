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
        public record NextCommand(Command Command) : ExecuteResult;
        public record NextCommands(List<Command> Commands) : ExecuteResult;
        public record End(ICommandResult Result) : ExecuteResult;
        public record Wait(UnityEvent Callback) : ExecuteResult;
    }

}