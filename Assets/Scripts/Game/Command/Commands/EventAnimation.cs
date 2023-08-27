using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System;
using TeamOdd.Ratocalypse.UI;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class EventAnimation : Command, ICommandRequire<PlayEvent>
    {
        private PlayEvent _playEvent;
        public void SetRequire(PlayEvent require)
        {
            _playEvent = require;
        }

        private string _name;

        public EventAnimation(string name)
        {
            _name = name;
        }

        public override ExecuteResult Execute()
        {
            var (endWait,result) = CreateWait();

            if(_name == "victory")
            {
                _playEvent.Show(0,()=>{
                    endWait(new End());
                });
            }
            if(_name == "defeat")
            {
                _playEvent.Show(1,()=>{
                    endWait(new End());
                });
            }
            if(_name =="nextround")
            {
                _playEvent.PlayLoop(2,()=>{
                    endWait(new End());
                });
            }
            return result;
        }

    }
}