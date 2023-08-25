using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.UI;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class CommandExecutor
    {
        public UnityEvent OnEmpty = new UnityEvent();

        private MapData _mapData;
        private GameStatistics _gameStatistics;

        private IMapSelector _catMapSelector;
        private IMapSelector _ratMapSelector;

        private ICardSelector _catHandSelector;
        private ICardSelector _ratHandSelector;

        private Stack<Command> _commands = new Stack<Command>();

        private TurnUI _turnUI;

        public CommandExecutor(MapData mapData, GameStatistics gameStatistics,
         IMapSelector catMapSelector, IMapSelector ratMapSelecetor,
         ICardSelector catHandSelector, ICardSelector ratHandSelector,
         TurnUI turnUI)
        {
            _mapData = mapData;
            _gameStatistics = gameStatistics;

            _catMapSelector = catMapSelector;
            _ratMapSelector = ratMapSelecetor;

            _catHandSelector = catHandSelector;
            _ratHandSelector = ratHandSelector;

            _turnUI = turnUI;
        }

        public void Clear()
        {
            _commands = new Stack<Command>();
        }

        public CommandExecutor Clone()
        {
            var cloned = MemberwiseClone() as CommandExecutor;
            cloned.OnEmpty = new UnityEvent();
            cloned.Clear();
            return cloned;
        }

        private void RunSubCommand(Command command)
        {
            _commands.Push(command);
            Execute(command);
        }

        private void Execute(Command command)
        {
            Debug.Log("Command Start: " + command.GetType().Name);
            SetRequires(command);
            ExecuteResult result = command.Execute();
            ProcessResult(result);
        }


        private void ProcessResult(ExecuteResult result)
        {
            switch (result)
            {
                case NextCommand:
                {
                    (Command next, ICommandResult commandResult) = result as NextCommand;
                    PopAndEnd(commandResult);
                    PushCommand(next);
                    Run();
                    break;
                }
                case NextCommands:
                {
                    (List<Command> nexts, ICommandResult commandResult) = result as NextCommands;
                    nexts.Reverse();
                    PopAndEnd(commandResult);
                    PushCommands(nexts);
                    Run();
                    break;
                }
                case Wait(UnityEvent<ExecuteResult> callback):
                {
                    callback.AddListener((result) => ProcessResult(result));
                    break;
                }
                case End:
                {
                    ICommandResult commandResult = (result as End).Result;
                    PopAndEnd(commandResult);
                    Run();
                    break;
                }
                case SubCommand(Command subCommand):
                {
                    RunSubCommand(subCommand);
                    break;
                }
                default:
                {
                    Debug.LogError("Unknown ExecuteResult" + result.GetType().Name);
                    break;
                }
            };
        }

        private void PopAndEnd(ICommandResult result)
        {
            var command = _commands.Pop();
            command.End(result);
            Debug.Log("Command End: " + command.GetType().Name);
        }

        private void SetRequires(Command command)
        {
            (command as IRequireMapSelectors)?.SetRequire((_ratMapSelector, _catMapSelector));
            (command as ICommandRequire<MapData>)?.SetRequire(_mapData);
            (command as ICommandRequire<GameStatistics>)?.SetRequire(_gameStatistics);
            (command as ICommandRequire<TurnUI>)?.SetRequire(_turnUI);
            (command as IRequireExtraExcutor)?.SetRequire(Clone);
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
                OnEmpty.Invoke();
                Debug.Log("No commands to run");
            }
        }
    }
}