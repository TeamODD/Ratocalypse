using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.TestScripts;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class GameInitializer:MonoBehaviour
    {
        private GameSequence _gameSequence;
        private CommandExecutor _commandExecutor;

        private GameStatistics _gameStatistics;

        [SerializeField]
        private TileSelector _tileSelector;
        [SerializeField]
        private Hand _handSelector;
        [SerializeField]
        private Map _map;

        private void Awake() 
        {
            Debug.Log("GameInitializer Awake");
            _commandExecutor = new CommandExecutor(_map.MapData,_gameStatistics,
                                                    _tileSelector, _tileSelector,
                                                    _handSelector, _handSelector);
            _gameSequence = new GameSequence(_map.MapData, _commandExecutor);
        }
        private void Start()
        {
            _gameSequence.Start();
        }
    }
}