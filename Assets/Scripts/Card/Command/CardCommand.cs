using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib;

namespace TeamOdd.Ratocalypse.CardLib.Command
{
    public class CardCommand
    {
        private Func<CardCastData, MapLib.GameLib.Command> _firstCommand;
        private List<Func<ICommandResult, MapLib.GameLib.Command>> _cardCommands;

        private int _current = 0;
        
        public CardCommand(Func<CardCastData, MapLib.GameLib.Command> command)
        {
            _cardCommands = new List<Func<ICommandResult, MapLib.GameLib.Command>>();
            _firstCommand = command;
        }

        public void AddCommand(Func<object, MapLib.GameLib.Command> command)
        {
            _cardCommands.Add(command);
        }

        public MapLib.GameLib.Command Next(ICommandResult parm)
        {
            MapLib.GameLib.Command next;
            if (_current == 0)
            {
                next = _firstCommand((CardCastData)parm);
            }
            else if (_current <= _cardCommands.Count)
            {
                next = _cardCommands[_current-1](parm);
            }
            else
            {
                next = null;   
            }
            _current++;

            return next;
        }


    }
}
