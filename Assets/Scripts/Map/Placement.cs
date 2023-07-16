using System.Collections.Generic;
using UnityEngine;

namespace TeamOdd.Ratocalypse.MapLib
{
    public partial class MapData
    {
        [System.Serializable]
        public class Placement
        {
            private MapData _mapData;
            [field: ReadOnly, SerializeField]
            public Vector2Int Coord { get; private set; }
            [field: SerializeField]//readonly but for test
            public List<Vector2Int> Shape { get; private set; }

            public Placement(MapData mapData, Vector2Int coord, List<Vector2Int> shape = null)
            {
                _mapData = mapData;
                Coord = coord;
                Shape = shape ?? new List<Vector2Int>() { Vector2Int.zero };
                SetCoord(coord);
            }

            public void SetCoord(Vector2Int newCoord)
            {
                Remove();
                Coord = newCoord;
                UpdatePosition();
            }

            public void Remove()
            {
                foreach (Vector2Int shapeCoord in Shape)
                {
                    Placement removed = _mapData.RemovePlaceable(Coord + shapeCoord);
                    if (removed != null && removed != this)
                    {
                        throw new System.Exception("cannot remove another placement");
                    }
                }
            }

            private void UpdatePosition()
            {
                foreach (Vector2Int shapeCoord in Shape)
                {
                    _mapData.SetPlaceble(Coord + shapeCoord, this);
                }
            }
        }
    }

}