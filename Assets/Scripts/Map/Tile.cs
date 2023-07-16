using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamOdd.Ratocalypse.MapLib
{
    public class Tile : MonoBehaviour
    {
        private TileData _tileData;
        public Vector2Int Coord => _tileData.Coord;

        private void Awake()
        {

        }

        private void Start()
        {

        }


        private void Update()
        {

        }

        public void Initiate(TileData tileData)
        {
            _tileData = tileData;
        }
    }
}