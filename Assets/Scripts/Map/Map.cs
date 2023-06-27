using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    [ExecuteInEditMode]
    public class Map : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _size = new Vector2Int(8, 8);

        [SerializeField]
        private float _tileDistance = 1.1f;

        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private Transform _tiles;

        public MapData MapData { get; private set; }

        private void Awake()
        {
            MapData = new MapData(_size);
            Init();
        }

        private Vector3 GetTilePosition(Vector2Int coord)
        {
            var mappedX = coord.x - ((float)MapData.Size.x - 1) / 2;
            var mappedY = coord.y - ((float)MapData.Size.y - 1) / 2;
            return new Vector3(mappedX, 0, mappedY) * _tileDistance;
        }

        private void Init()
        {
            for (int i = _tiles.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(_tiles.GetChild(i).gameObject);
            }
            for (int x = 0; x < MapData.Size.x; x++)
            {
                for (int y = 0; y < MapData.Size.y; y++)
                {
                    var Coord = new Vector2Int(x, y);
                    var position = GetTilePosition(Coord);
                    var tile = Instantiate(_tilePrefab, _tiles, false);
                    tile.transform.localPosition = position;
                    tile.name = "Tile " + Coord;
                    tile.Initiate(Coord);
                }
            }
        }

        private void Update()
        {

        }
    }
}