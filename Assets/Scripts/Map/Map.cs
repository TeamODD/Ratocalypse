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
        private float _tileDistance = 1.1f;

        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private Transform _tiles;

        [field: SerializeField]
        public MapCoord MapCoord{ get; private set; }

        public MapData MapData { get; private set; }

        private void Awake()
        {
            UpdateTiles();
        }

        private void UpdateTiles()
        {
            RemoveTiles();
            MapData = new MapData(MapCoord.Size);
            CreateTiles();
        }

        private void CreateTiles()
        {
            for (int x = 0; x < MapData.Size.x; x++)
            {
                for (int y = 0; y < MapData.Size.y; y++)
                {
                    var coord = new Vector2Int(x, y);
                    var position = MapCoord.GetTilePosition(coord);
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