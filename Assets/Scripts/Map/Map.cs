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
            UpdateTiles();
        }

        private Vector3 GetTilePosition(Vector2Int coord)
        {
            var mappedX = coord.x - ((float)MapData.Size.x - 1) / 2;
            var mappedY = coord.y - ((float)MapData.Size.y - 1) / 2;
            return new Vector3(mappedX, 0, mappedY) * _tileDistance;
        }

        private void UpdateTiles()
        {
            RemoveTiles();
            MapData = new MapData(_size);
            CreateTiles();
        }

        private void CreateTiles()
        {
            for (int x = 0; x < MapData.Size.x; x++)
            {
                for (int y = 0; y < MapData.Size.y; y++)
                {
                    var coord = new Vector2Int(x, y);
                    var position = GetTilePosition(coord);
                    var tile = Instantiate(_tilePrefab, _tiles, false);
                    tile.transform.localPosition = position;
                    tile.name = "Tile " + coord;

                    var tileData = MapData.GetTile(coord);
                    tile.Initiate(tileData);
                }
            }
        }

        private void RemoveTiles()
        {
            for (int i = _tiles.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(_tiles.GetChild(i).gameObject);
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
    }
}