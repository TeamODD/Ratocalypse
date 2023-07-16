using UnityEngine;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class TileData
    {
        public Vector2Int Coord { get; }

        public TileData(Vector2Int coord)
        {
            Coord = coord;
        }

    }
}