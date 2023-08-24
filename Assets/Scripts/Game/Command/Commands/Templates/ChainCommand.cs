using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.CardLib.CommandLib
{
    public class ChainCommand : Command
    {
        protected List<Func<ICommandResult, Command>> _commands;
        protected int _current = 0;

        protected ICommandResult _endResult;

        protected ICommandResult _parm;

        public ChainCommand(ICommandResult firstParm = null)
        {
            _parm = firstParm;
            _commands = new List<Func<ICommandResult, Command>>();
        }

        public void AddCommand(Func<ICommandResult, Command> command)
        {
            _commands.Add(command);
        }

        public Command Next(ICommandResult parm)
        {
            Command nextCommand = null;
            if (_current < _commands.Count)
            {
                nextCommand = _commands[_current](parm);
                _current++;
                if (nextCommand == null)
                {
                    return Next(_parm);
                }
            }
            return nextCommand;
        }

        protected void SetParm(ICommandResult parm)
        {
            _parm = parm;
        }

        private Command Jump(int step, ICommandResult parm = null)
        {
            _current += step;
            return null;
        }

        protected Command EndCommand(ICommandResult result)
        {
            _endResult = result;
            return null;
        }

        public override ExecuteResult Execute()
        {
            if (_endResult != null)
            {
                return new End(_endResult);
            }

            Command nextCommand = Next(_parm);
            if (nextCommand != null)
            {
                nextCommand.RegisterOnEnd(SetParm);
                return new SubCommand(nextCommand);
            }
            else
            {
                return new End(_parm);
            }
        }
    }
}
