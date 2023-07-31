using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class CommandExecutor
    {
        private MapData _mapData;
        private GameStatistics _gameStatistics;

        private ISelector _catSelector;
        private ISelector _ratSelector;

        private Stack<Command> _commands = new Stack<Command>();

        public CommandExecutor(MapData mapData, GameStatistics gameStatistics, ISelector catSelector, ISelector ratSelector)
        {
            _mapData = mapData;
            _gameStatistics = gameStatistics;
            _catSelector = catSelector;
            _ratSelector = ratSelector;
        }

        private void RunSubCommand(Command command)
        {
            _commands.Push(command);
            Execute(command);
        }

        private void OnEndCommand(Command command)
        {
            _commands.Pop();
            Debug.Log("Command End: " + command.GetType().Name);
            Run();
        }

        private void Execute(Command command)
        {
            SetRequires(command);
            ExecuteResult result = command.Execute();
            ProcessResult(result);
        }


        private void ProcessResult(ExecuteResult result)
        {
            switch(result)
            {
                case NextCommand(Command next):
                    PushCommand(next);
                    break;

                case NextCommands(List<Command> nexts):
                    PushCommands(nexts);
                    break;

                case Wait(UnityEvent callback):
                    _commands.Pop();
                    callback.AddListener(Run);
                    break;

                case End:
                    _commands.Pop();
                    Run();
                    break;

                case SubCommand(Command subCommand):
                    RunSubCommand(subCommand);
                    break;

                default:
                    Debug.LogError("Unknown ExecuteResult"+result.GetType().Name);
                    break;
            };
        }

        private void SetRequires(Command command)
        {
            (command as IRequireSelectors)?.SetRequire((_ratSelector, _catSelector));
            (command as ICommandRequire<MapData>)?.SetRequire(_mapData);
            (command as ICommandRequire<GameStatistics>)?.SetRequire(_gameStatistics);
        }

        public void PushCommand(Command command)
        {
            _commands.Push(command);
        }

        public void PushCommands(List<Command> commands)
        {
            commands.ForEach((command) => _commands.Push(command));
        }

        public void Run()
        {
            if (_commands.Count > 0)
            {
                Execute(_commands.Peek());
            }
            else
            {
                Debug.Log("No commands to run");
            }
        }
    }
}