using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;
using static TeamOdd.Ratocalypse.TestScripts.TileColorSetter;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;

namespace TeamOdd.Ratocalypse.TestScripts
{
    [RequireComponent(typeof(Map))]
    public class TileSelector : MonoBehaviour
    {

        private Map _map;
        private MapAnalyzer _analyzer;

        private ShapedCoordList _currentCandidates;

        private void Start()
        {
            _map = GetComponent<Map>();
            _analyzer = new MapAnalyzer(_map.MapData);
        }

        public void Select(Selection selection)
        {
            _currentCandidates = selection.TileCandidates;
            var tileSelectionMap = selection.TileSelectionMap;
            foreach (Vector2Int coord in selection.TileSelectionMap.Keys)
            {
                Tile tile = _map.GetTile(coord);
                TileCallback tileCallback = tile.GetComponent<TileCallback>();
                HightLightTile(tile, TileColor.Blue);
                int index = tileSelectionMap[tile.Coord];
                tileCallback.ClickEvent.AddListener((_) => Reset());
                tileCallback.ClickEvent.AddListener((Tile tile) =>
                {
                    selection.SelectTile(index);
                });

                List<Vector2Int> tiles = _currentCandidates.GetCoords(index);
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

            if(!selection.HasTileSelection)
            {
                return;
            }
            
            for(int i = 0; i < selection.PlacementCandidates.Count; i++)
            {
                Placement placement = selection.PlacementCandidates[i];
                if (placement is not IDamageable damagable)
                {
                    continue;
                }

                List<Vector2Int> coords = placement.Shape.GetCoords(placement.Coord);
                coords.ForEach((coord) => {
                    Tile tile = _map.GetTile(coord);
                    HightLightTile(coord, TileColor.Blue);
                    _currentCandidates.Add(coord);
                    TileCallback tileCallback = tile.GetComponent<TileCallback>();

                    int index = i;
                    tileCallback.ClickEvent.AddListener((_) => Reset());
                    tileCallback.ClickEvent.AddListener((_) =>
                    {
                        selection.SelectPlacement(index);
                    });

                    tileCallback.EnterEvent.AddListener((_) =>
                    {
                        foreach (Vector2Int point in coords)
                        {
                            HightLightTile(point, TileColor.Red);
                        }
                    });

                    tileCallback.ExitEvent.AddListener((_) =>
                    {
                        foreach (Vector2Int point in coords)
                        {
                            HightLightTile(point, TileColor.Blue);
                        }
                    });
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