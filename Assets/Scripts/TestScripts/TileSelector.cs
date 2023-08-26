using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;
using static TeamOdd.Ratocalypse.TestScripts.TileColorSetter;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;

namespace TeamOdd.Ratocalypse.TestScripts
{
    [RequireComponent(typeof(Map))]
    public class TileSelector : MonoBehaviour, IMapSelector
    {

        private Map _map;
        private MapAnalyzer _analyzer;

        private ShapedCoordList _coords;
        private List<Placement> _placements;
        private bool _selectBoth = false;
        private void Start()
        {
            _map = GetComponent<Map>();
            _analyzer = new MapAnalyzer(_map.MapData);
        }

        public void HightLightTile(Tile tile, TileColor color)
        {
            tile.GetComponent<TileColorSetter>().SetColor(color);
        }

        public void HightLightTile(Vector2Int coord, TileColor color)
        {
            _map.GetTile(coord).GetComponent<TileColorSetter>().SetColor(color);
        }

        public void Preview(MapSelecting mapSelecting)
        {
            mapSelecting.Calculate(_map.MapData);
            var allCoords = mapSelecting.CoordCandidates;
            foreach(List<Vector2Int> coords in allCoords)
            {
                foreach(Vector2Int coord in coords)
                {
                    HightLightTile(coord, TileColor.Red);
                }
            }
            mapSelecting.PlacementCanditates.ForEach((placement) =>
            {
                foreach (Vector2Int coord in placement.Shape.GetCoords(placement.Coord))
                {
                    HightLightTile(coord, TileColor.Red);
                }
            });
        }

        public void Reset()
        {
            ResetHighlight();
            ResetHandlers();
            _coords = null;
            _placements = null;
        }


        public void ResetHandlers()
        {
            for (int y = 0; y < _map.Size.y; y++)
            {
                for (int x = 0; x < _map.Size.x; x++)
                {
                    _map.GetTile(new Vector2Int(x, y)).GetComponent<TileCallback>().RemoveAll();
                }
            }
        }

        public void ResetHighlight()
        {
            for (int y = 0; y < _map.Size.y; y++)
            {
                for (int x = 0; x < _map.Size.x; x++)
                {
                    _map.GetTile(new Vector2Int(x, y)).GetComponent<TileColorSetter>().Default();
                }
            }
        }

        public void Select(Selection<ShapedCoordList> selection)
        {
            if (!_selectBoth)
            {
                var count = selection.GetCandidates().Count;
                if (count == 0)
                {
                    selection.Select(-1);
                    return;
                }
            }

            _coords = selection.GetCandidates();

            for (int i = 0; i < _coords.Count; i++)
            {
                foreach (Vector2Int coord in _coords.GetCoords(i))
                {
                    int index = i;
                    Tile tile = _map.GetTile(coord);
                    TileCallback tileCallback = tile.GetComponent<TileCallback>();
                    HightLightTile(tile, TileColor.Blue);
                    tileCallback.RemoveAll();
                    tileCallback.ClickEvent.AddListener((_) => Reset());
                    tileCallback.ClickEvent.AddListener((Tile tile) =>
                    {
                        selection.Select(index);
                    });

                    List<Vector2Int> tiles = _coords.GetCoords(i);
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
        }

        public void Select(Selection<List<Placement>> selection)
        {
            if (!_selectBoth)
            {
                var count = selection.GetCandidates().Count;
                if (count == 0)
                {
                    selection.Select(-1);
                    return;
                }
            }

            _placements = selection.GetCandidates();
            for (int i = 0; i < _placements.Count; i++)
            {
                Placement placement = _placements[i];
                List<Vector2Int> coords = placement.Shape.GetCoords(placement.Coord);
                foreach (Vector2Int coord in coords)
                {
                    Tile tile = _map.GetTile(coord);
                    HightLightTile(coord, TileColor.Blue);

                    TileCallback tileCallback = tile.GetComponent<TileCallback>();

                    int index = i;
                    tileCallback.ClickEvent.AddListener((_) => Reset());
                    tileCallback.ClickEvent.AddListener((_) =>
                    {
                        selection.Select(index);
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

                }
            }

        }

        public void Select(Selection<ShapedCoordList> coordSelection, Selection<List<Placement>> placementSelection)
        {
            _selectBoth = true;
            Select(coordSelection);
            Select(placementSelection);
            _selectBoth = false;
            var count = _coords.Count + _placements.Count;
            if (count == 0)
            {
                coordSelection.Select(-1);
            }

        }
    }
}