using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.TestScripts;
using TeamOdd.Ratocalypse.UI;
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

        [SerializeField]
        private TurnUI _turnUI;
        [SerializeField]
        private RatIconFactory _ratIconFactory;
        [SerializeField]
        private CatIconFactory _catIconFactory;

        private void Awake() 
        {
            Debug.Log("GameInitializer Awake");
            _commandExecutor = new CommandExecutor(_map.MapData,_gameStatistics,
                                                    _tileSelector, _tileSelector,
                                                    _handSelector, _handSelector,
                                                    _turnUI);
            _gameSequence = new GameSequence(_map.MapData, _commandExecutor);
        }
        private void Start()
        {
            _map.MapData.GetPlacements().ForEach((placement) => 
            {
                if(placement is RatData ratData)
                {
                    var icon =_ratIconFactory.Create(_turnUI.transform, ratData);
                    _turnUI.AddIcon(icon);
                }
                else if(placement is CatData catData)
                {
                    var icon =_catIconFactory.Create(_turnUI.transform, catData);
                    _turnUI.AddIcon(icon);
                }
            });
            _turnUI.UpdatePositions();
            _gameSequence.Start();
        }
    }
}