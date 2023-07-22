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
        public int Count { get => Coords.Count; }
        public ShapedCoordList(Shape shape)
        {
            Shape = shape;
            Coords = new List<Vector2Int>();
        }

        public ShapedCoordList(Shape shape, List<Vector2Int> coords)
        {
            Shape = shape;
            Coords = coords;
        }

        public void Add(Vector2Int coord)
        {
            Coords.Add(coord);
        }

        public void AddRange(List<Vector2Int> coords)
        {
            Coords.AddRange(coords);
        }

        public void RemoveAt(int index)
        {
            Coords.RemoveAt(index);
        }

        public IEnumerator<List<Vector2Int>> GetEnumerator()
        {
            foreach (Vector2Int coord in Coords)
            {
                yield return Shape.GetCoords(coord);
            }
        }

        public Vector2Int GetCoord(int index)
        {
            return Coords[index];
        }

        public List<Vector2Int> GetCoords(int index)
        {
            return Shape.GetCoords(Coords[index]);
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