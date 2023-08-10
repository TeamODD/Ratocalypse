using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib.CommandLib
{
    public class CardCommand
    {
        public enum CardCommandType
        {
            Start,
            Cast,
            EndCast,
            Trigger,
            EndTrigger,
        }

        private Func<CardCastData, Command> _firstCommand;
        private List<Func<ICommandResult, Command>> _castCommands;
        private List<Func<ICommandResult, Command>> _triggerCommands;

        private CardCommandType _currentType = CardCommandType.Start;

        private int _current  = 0;

        public CardCommand(Func<CardCastData, Command> command)
        {
            _castCommands = new List<Func<ICommandResult, Command>>();
            _triggerCommands = new List<Func<ICommandResult, Command>>();
            _firstCommand = command;
        }

        public void AddCastCommand(Func<object, Command> command)
        {
            _castCommands.Add(command);
        }

        public void AddTriggerCommand(Func<object, Command> command)
        {
            _triggerCommands.Add(command);
        }

        public (CardCommandType commandType, Command command) Next(ICommandResult parm)
        {
            (CardCommandType,Command) next;
            
            if(_currentType == CardCommandType.Start)
            {
                _currentType = CardCommandType.Cast;
                next = (CardCommandType.Cast, _firstCommand((CardCastData)parm));
            }
            else if (_currentType == CardCommandType.Cast)
            {
                if(_current<_castCommands.Count)
                {
                    Command nextCommand = _triggerCommands[_current](parm);
                    next = (CardCommandType.Cast, nextCommand);
                    _current++;
                }
                else
                {
                    _currentType = CardCommandType.Trigger;
                    _current = 0;
                    next = (CardCommandType.EndCast, null);
                }
            }
            else
            {
                if(_current<_triggerCommands.Count)
                {
                    Command nextCommand = _triggerCommands[_current](parm);
                    next = (CardCommandType.Trigger, nextCommand);
                    _current++;
                }
                else
                {
                    _currentType = CardCommandType.Trigger;
                    next = (CardCommandType.EndTrigger, null);
                }
            }

            return next;
        }

    }
}
