using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib
{
    public partial class MapData
    {
        public Vector2Int Size { get; }
        private List<List<Placement>> _placedMap;
        private HashSet<Placement> _placements = new HashSet<Placement>();

        public MapData(Vector2Int size)
        {
            Size = size;
            ResetMap();
        }

        public MapData(int width = 8, int height = 8)
        {
            Size = new Vector2Int(width, height);
            ResetMap();
        }

        public List<Placement> GetPlacements()
        {
            return _placements.ToList();
        }

        private void ResetMap()
        {
            _placedMap = new List<List<Placement>>();
            _placements.Clear();
            for (int y = 0; y < Size.y; y++)
            {
                _placedMap.Add(new List<Placement>());
                for (int x = 0; x < Size.x; x++)
                {
                    _placedMap[y].Add(null);
                }
            }
        }

        public Placement GetPlacement(Vector2Int coord)
        {
            return _placedMap[coord.y][coord.x];
        }

        public bool IsExist(Vector2Int coord)
        {
            return GetPlacement(coord) != null;
        }

        private Placement RemovePlaceable(Vector2Int coord)
        {
            Placement exist = GetPlacement(coord);
            _placedMap[coord.y][coord.x] = null;

            _placements.Remove(exist);

            return exist;
        }

        private void SetPlaceble(Vector2Int coord, Placement placement, bool force = false)
        {
            
            if (!force && placement != null && GetPlacement(coord) != null)
            {
                throw new System.Exception("placement is already exist");
            }
            if (placement == null)
            {
                throw new System.Exception("use RemovePlaceable instead of SetPlaceble");
            }

            _placements.Add(placement);

            _placedMap[coord.y][coord.x] = placement;

        }

        public void Print()
        {
            string line = "";
            for (int y = 0; y < Size.y; y++)
            {

                for (int x = 0; x < Size.x; x++)
                {
                    line += _placedMap[y][x] == null ? "0" : "1";
                }
                line += "\n";
            }
            Debug.Log(line);
        }
    }

}