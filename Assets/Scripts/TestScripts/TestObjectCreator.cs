using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.ObstacleLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.Obstacle;
using System.Linq;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.DeckLib;

namespace TeamOdd.Ratocalypse.TestScripts
{
    public class TestObjectCreator : MonoBehaviour
    {
        [SerializeField]
        private PlacementObjectFactory _factory;

        [SerializeField]
        private PlacementObject _prefab;
        
        private enum ObjectType
        {
            Rat,
            Cat,
            Obstacle,
        }

        [SerializeField]
        private ObjectType _objectType;

        [SerializeField]
        private List<Vector2Int> _shapeList;

        [SerializeField]
        private Map _map;

        [SerializeField]
        private int _maxHp = 100;

        [SerializeField]
        private int _maxStamina = 100;

        [SerializeField]
        private Hand _hand;

        [SerializeField]
        private List<int> _testDeck = new List<int>();
        [SerializeField]
        private bool _createOnStart = false;

        [SerializeField]
        private List<Vector2Int> _coords;


        public Placement CreateData(Vector2Int coord)
        {
            return _objectType switch
            {
                ObjectType.Rat => new RatData(_maxHp, _maxStamina, _map.MapData, coord,_testDeck,_hand),
                ObjectType.Cat => new CatData(_maxHp, _maxStamina, _map.MapData, coord,_testDeck,_hand),
                ObjectType.Obstacle => new ObstacleData(_maxHp, _map.MapData, coord, new Shape(_shapeList)),
                _ => null,
            };
        }

        public PlacementObject CreateObject(Vector2Int coord)
        {
            return _factory.Create(CreateData(coord),_prefab);
        }

        private void Awake()
        {
            if (_createOnStart)
            {
                foreach (Vector2Int coord in _coords)
                {
                    CreateObject(coord);
                }
            }
        }
    }
}