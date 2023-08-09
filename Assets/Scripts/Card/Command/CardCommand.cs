using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib.Command
{
    public class CardCommand
    {
        private Func<CardCastData, MapLib.GameLib.Command> _firstCommand;
        private List<Func<ICommandResult, MapLib.GameLib.Command>> _cardCommands;

        public int Current { get; private set; } = 0;

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
            if (Current == 0)
            {

                next = _firstCommand((CardCastData)parm);
            }
            else if (Current <= _cardCommands.Count)
            {
                next = _cardCommands[Current - 1](parm);
            }
            else
            {
                next = null;
            }
            Current++;

            return next;
        }


    }
}
