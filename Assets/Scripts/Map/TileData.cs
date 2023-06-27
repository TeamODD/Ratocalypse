using UnityEngine;

namespace TeamOdd.Ratocalypse.Map
{
    public class TileData
    {
        public Vector2Int Coord {get;}
        public IPlaceable Placeable { get; private set; }

        public TileData(Vector2Int coord, IPlaceable placeable = null)
        {
            Coord = coord;
            Placeable = placeable;
        }

        public IPlaceable RemovePlaceable()
        {
            IPlaceable temp = Placeable;
            Placeable = null;
            return temp;
        }

        public void SetPlaceable(IPlaceable placeable)
        {
            Placeable = placeable;
        }
    }
}