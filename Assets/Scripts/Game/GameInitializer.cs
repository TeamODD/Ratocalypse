using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.Obstacle;
using TeamOdd.Ratocalypse.TestScripts;
using TeamOdd.Ratocalypse.UI;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class GameInitializer : MonoBehaviour
    {
        private GameSequence _gameSequence;
        private CommandExecutor _commandExecutor;

        private GameStatistics _gameStatistics = new GameStatistics();

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

        [SerializeField]
        private RatFactory _ratFactory;
        [SerializeField]
        private CatFactory _catFactory;
        [SerializeField]
        private PlacementObjectFactory _obstacleFactory;


        [System.Serializable]
        public class RatCreateData
        {
            public int RatType;
            public List<int> cards;
            public int Maxhp;
            public int MaxStatmina;
            public List<Vector2Int> PositionShape;
        }

        [System.Serializable]
        public class CatCreateData
        {
            public List<int> cards;
            public int Maxhp;
            public int MaxStatmina;
        }

        [System.Serializable]
        public class ObstacleCreateData
        {
            public int Maxhp;
            public PlacementObject ObstaclePrefab;
            public List<Vector2Int> ShapeCoords;
        }



        [SerializeField]
        private List<RatCreateData> _ratCreateDatas = new List<RatCreateData>();

        [SerializeField]
        private CatCreateData _catCreateData = new CatCreateData();

        [SerializeField]
        private List<ObstacleCreateData> _obstacleCreateData = new List<ObstacleCreateData>();

        private void CreateRats()
        {
            _ratCreateDatas.ForEach((ratCreateData) =>
            {
                var mapAnalyzer = new MapAnalyzer(_map.MapData);
                var shape = new Shape(ratCreateData.PositionShape);
                var coord = mapAnalyzer.GetRandomCoord(shape);

                var ratData = new RatData(ratCreateData.Maxhp, ratCreateData.MaxStatmina,
                 _map.MapData, coord, ratCreateData.cards, _handSelector);
                _ratFactory.Create(ratData, ratCreateData.RatType);
            });
        }

        private void CreateObstacles()
        {
            _obstacleCreateData.ForEach((obstacleCreateData) =>
            {
                var mapAnalyzer = new MapAnalyzer(_map.MapData);
                var shape = new Shape(obstacleCreateData.ShapeCoords);
                var coord = mapAnalyzer.GetRandomCoord(shape);

                var obstacleData = new ObstacleData(obstacleCreateData.Maxhp,
                 _map.MapData, coord, shape);
                 _obstacleFactory.Create(obstacleData, obstacleCreateData.ObstaclePrefab);
            });
        }

        private void CreateCat()
        {
            var coord = new Vector2Int((_map.Size.x - 1) / 2, (_map.Size.y - 1) / 2);

            var catData = new CatData(_catCreateData.Maxhp, _catCreateData.MaxStatmina,
             _map.MapData, coord, _catCreateData.cards, _handSelector);
            _catFactory.Create(catData);
        }


        private void Awake()
        {

            CreateCat();
            CreateRats();
            CreateObstacles();

            Debug.Log("GameInitializer Awake");
            _commandExecutor = new CommandExecutor(_map.MapData, _gameStatistics,
                                                    _tileSelector, _tileSelector,
                                                    _handSelector, _handSelector,
                                                    _turnUI);
            _gameSequence = new GameSequence(_map.MapData, _commandExecutor);
        }

        private void Start()
        {
            Debug.Log("?");
            _map.MapData.GetPlacements().ForEach((placement) =>
            {
                if (placement is RatData ratData)
                {
                    var icon = _ratIconFactory.Create(_turnUI.transform, ratData);
                    _turnUI.AddIcon(icon);
                }
                else if (placement is CatData catData)
                {
                    var icon = _catIconFactory.Create(_turnUI.transform, catData);
                    _turnUI.AddIcon(icon);
                }
            });
            _turnUI.UpdatePositions();
            _gameSequence.Start();
        }
    }
}