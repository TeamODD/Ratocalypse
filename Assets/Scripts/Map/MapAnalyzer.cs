using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class MapAnalyzer
    {
        private MapData _mapData;

        public MapAnalyzer(MapData mapData)
        {
            _mapData = mapData;
        }

        public bool CheckInbound(Vector2Int coord)
        {
            Vector2Int size = _mapData.Size;
            if (coord.x < 0 || coord.x >= size.x || coord.y < 0 || coord.y >= size.y)
            {
                return false;
            }
            return true;
        }

        public void RemoveOutBounds(List<Vector2Int> coords)
        {
            for (int i = 0; i < coords.Count; i++)
            {
                if (!CheckInbound(coords[i]))
                {
                    coords.RemoveAt(i);
                    i--;
                }
            }
        }

        public List<Vector2Int> GetCoordsInDistance(Vector2Int coord, int distance, Func<Vector2Int, bool> match = null)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            for (int x = -distance; x <= distance; x++)
            {
                for (int y = -distance; y <= distance; y++)
                {
                    Vector2Int newCoord = new Vector2Int(coord.x + x, coord.y + y);
                    if (CheckInbound(newCoord))
                    {
                        result.Add(newCoord);
                    }
                }
            }

            return WhereIn(result, match);

        }

        public List<Placement> GetPlacementsInDistance(Vector2Int coord, int distance, Func<Placement, bool> match = null)
        {
            List<Vector2Int> coords = GetCoordsInDistance(coord, distance);
            return WhereIn(coords, match);
        }

        public List<Placement> GetPlacementsFrom(List<Vector2Int> coords)
        {
            List<Placement> placements = new List<Placement>();
            coords.ForEach((Vector2Int coord) =>
            {
                Placement placement = _mapData.GetPlacement(coord);
                if (placement != null)
                {
                    placements.Add(placement);
                }
            });

            return placements;
        }

        public List<Vector2Int> WhereIn(List<Vector2Int> coords, Func<Vector2Int, bool> match)
        {
            if (match == null)
            {
                return coords;
            }
            return coords.Where((Vector2Int coord) => match(coord)).ToList();
        }

        public List<Placement> WhereIn(List<Vector2Int> coords, Func<Placement, bool> match)
        {
            if (match == null)
            {
                return GetPlacementsFrom(coords);
            }
            List<Vector2Int> filteredCoords = coords.FindAll((Vector2Int coord) => match(_mapData.GetPlacement(coord))).ToList();
            return GetPlacementsFrom(filteredCoords);
        }

        public List<Vector2Int> All()
        {
            List<Vector2Int> coords = new List<Vector2Int>();
            for (int x = 0; x < _mapData.Size.x; x++)
            {
                for (int y = 0; y < _mapData.Size.y; y++)
                {
                    coords.Add(new Vector2Int(x, y));
                }
            }
            return coords;
        }

        public List<Placement> Where(Func<Placement, bool> match)
        {
            return WhereIn(All(), match);
        }

        public List<Vector2Int> Where(Func<Vector2Int, bool> match)
        {
            return WhereIn(All(), match);
        }

        public int GetDistance(Vector2Int from, Vector2Int to)
        {
            return Mathf.Max(Mathf.Abs(from.x - to.x) , Mathf.Abs(from.y - to.y));
        }

        public bool CheckAllIn(List<Vector2Int> coords, Func<Vector2Int, Placement, bool> filter)
        {
            foreach (Vector2Int coord in coords)
            {
                Placement placement = _mapData.GetPlacement(coord);
                if (!filter(coord,placement))
                {
                    return false;
                }
            }
            return true;
        }

    }
}