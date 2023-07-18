using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TeamOdd.Ratocalypse.MapLib
{
    [ExecuteInEditMode]
    public class Map : MonoBehaviour, IMapCoord
    {
        [SerializeField]
        private float _tileDistance = 1.1f;

        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private Transform _tileParent;

        [field: SerializeField]
        public Vector2Int Size { get; private set; }

        public MapData MapData { get; private set; }

        private Tile[,] _tiles;

        private void Awake()
        {
            UpdateTiles();
        }

        private void UpdateTiles()
        {
            RemoveTiles();
            MapData = new MapData(Size);
            CreateTiles();
        }

        private void CreateTiles()
        {
            _tiles = new Tile[MapData.Size.y, MapData.Size.x];

            for (int x = 0; x < MapData.Size.x; x++)
            {
                for (int y = 0; y < MapData.Size.y; y++)
                {
                    var coord = new Vector2Int(x, y);
                    var position = GetTileLocalPosition(coord);
                    var tile = Instantiate(_tilePrefab, _tileParent, false);
                    tile.transform.localPosition = position;
                    tile.name = "Tile " + coord;

                    var tileData = new TileData(coord);
                    tile.Initiate(tileData);
                    _tiles[y, x] = tile;
                }
            }
        }

        private void RemoveTiles()
        {
            for (int i = _tileParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(_tileParent.GetChild(i).gameObject);
                _tiles = null;
            }
        }

        private void OnValidate()
        {
            StartCoroutine(UpdateMap());
        }

        private IEnumerator UpdateMap()
        {
            yield return null;
            UpdateTiles();
        }

        public Tile GetTile(Vector2Int coord)
        {
            return _tiles[coord.y, coord.x];
        }

        public Vector3 GetTileWorldPosition(Vector2Int coord)
        {
            return _tiles[coord.y, coord.x].transform.position;
        }

        public Vector3 GetTileLocalPosition(Vector2Int coord)
        {
            var mappedX = coord.x - ((float)Size.x - 1) / 2;
            var mappedY = coord.y - ((float)Size.y - 1) / 2;
            var newPosition = new Vector3(mappedX, 0, mappedY) * _tileDistance;
            return newPosition;
        }
    }
}