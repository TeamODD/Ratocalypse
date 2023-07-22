using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class ShapedCoordList : IEnumerable<List<Vector2Int>>
    {
        public Shape Shape;
        public List<Vector2Int> Coords;

        public ShapedCoordList(Shape shape, List<Vector2Int> coords)
        {
            Shape = shape;
            Coords = coords;
        }

        public IEnumerator<List<Vector2Int>> GetEnumerator()
        {
            foreach (Vector2Int coord in Coords)
            {
                yield return Shape.GetCoords(coord);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Vector2Int coord in Coords)
            {
                yield return Shape.GetCoords(coord);
            }
        }
    }

}