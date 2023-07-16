using UnityEngine;
using System.Collections.Generic;

namespace TeamOdd.Ratocalypse.MapLib
{
    public partial class MapData
    {
        public Vector2Int Size { get; }
        public List<List<Placement>> Placements { get; private set; }

        public MapData(Vector2Int size)
        {
            Size = size;
            ResetPlacements();
        }

        public MapData(int width = 8, int height = 8)
        {
            Size = new Vector2Int(width, height);
            ResetPlacements();
        }

        private void ResetPlacements()
        {
            Placements = new List<List<Placement>>();
            for (int y = 0; y < Size.y; y++)
            {
                Placements.Add(new List<Placement>());
                for (int x = 0; x < Size.x; x++)
                {
                    Placements[y].Add(null);
                }
            }
        }

        public Placement GetPlacement(Vector2Int coord)
        {
            return Placements[coord.y][coord.x];
        }

        private Placement RemovePlaceable(Vector2Int coord)
        {
            Placement exist = GetPlacement(coord);
            Placements[coord.y][coord.x] = null;
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
                Debug.Log("use RemovePlaceable instead of SetPlaceble");
            }

            Placements[coord.y][coord.x] = placement;

        }
    }

}