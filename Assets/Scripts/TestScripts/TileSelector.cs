using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;
using static TeamOdd.Ratocalypse.TestScripts.TileColorSetter;

namespace TeamOdd.Ratocalypse.TestScripts
{
    [RequireComponent(typeof(Map))]
    public class TileSelector : MonoBehaviour
    {

        private Map _map;
        private MapAnalyzer _analyzer;

        private List<List<Vector2Int>> _currentCandidates;

        private void Start()
        {
            _map = GetComponent<Map>();
            _analyzer = new MapAnalyzer(_map.MapData);
        }

        public void Select(List<List<Vector2Int>> candidates, Dictionary<Vector2Int, int> selectionMap, Action<int> callback)
        {
            _currentCandidates = candidates;
            foreach (Vector2Int coord in selectionMap.Keys)
            {
                Tile tile = _map.GetTile(coord);
                TileCallback tileCallback = tile.GetComponent<TileCallback>();
                HightLightTile(tile, TileColor.Blue);
                int index = selectionMap[tile.Coord];
                tileCallback.ClickEvent.AddListener((_) => Reset());
                tileCallback.ClickEvent.AddListener((Tile tile) =>
                {
                    callback?.Invoke(index);
                });

                List<Vector2Int> tiles = candidates[index];
                tileCallback.EnterEvent.AddListener((Tile tile) =>
                {
                    foreach (Vector2Int point in tiles)
                    {
                        HightLightTile(_map.GetTile(point), TileColor.Yellow);
                    }
                });
                tileCallback.ExitEvent.AddListener((Tile tile) =>
                {
                    foreach (Vector2Int point in tiles)
                    {
                        HightLightTile(_map.GetTile(point), TileColor.Blue);
                    }
                });

            }
        }

        public void HightLightTile(Tile tile, TileColor color)
        {
            tile.GetComponent<TileColorSetter>().SetColor(color);
        }
        public void HightLightTile(Vector2Int coord, TileColor color)
        {
            _map.GetTile(coord).GetComponent<TileColorSetter>().SetColor(color);
        }

        public void Reset()
        {
            ResetHighlight();
            ResetHandlers();
            _currentCandidates = null;
        }


        public void ResetHandlers()
        {
            foreach (List<Vector2Int> coords in _currentCandidates)
            {
                foreach (Vector2Int coord in coords)
                {
                    _map.GetTile(coord).GetComponent<TileCallback>().ClickEvent.RemoveAllListeners();
                    _map.GetTile(coord).GetComponent<TileCallback>().EnterEvent.RemoveAllListeners();
                    _map.GetTile(coord).GetComponent<TileCallback>().ExitEvent.RemoveAllListeners();
                }
            }
        }

        public void ResetHighlight()
        {
            foreach (List<Vector2Int> coords in _currentCandidates)
            {
                foreach (Vector2Int coord in coords)
                {
                    _map.GetTile(coord).GetComponent<TileColorSetter>().Default();
                }
            }
        }
    }
}