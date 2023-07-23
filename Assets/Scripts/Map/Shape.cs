using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace TeamOdd.Ratocalypse.MapLib
{
    [System.Serializable]
    public class Shape : IEnumerable<Vector2Int>
    {
        [SerializeField]
        private List<Vector2Int> _coords = new List<Vector2Int>();
        public Vector2Int Origin { get => _coords[0]; }

        public Shape()
        {
            _coords.Add(Vector2Int.zero);
        }

        public Shape(List<Vector2Int> coords)
        {
            _coords = coords;
        }

        public Shape(int x, int y)
        {
            AddRect(Vector2Int.zero, new Vector2Int(x, y));
        }

        public Shape(Vector2Int size)
        {
            AddRect(Vector2Int.zero, size);
        }

        public Shape Copy()
        {
            return new Shape(_coords.Select(coord => coord).ToList());
        }

        public void AddRect(Vector2Int offset, Vector2Int size)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    _coords.Add(offset + new Vector2Int(x, y));
                }
            }
        }

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            return _coords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _coords.GetEnumerator();
        }

        public List<Vector2Int> GetCoords()
        {
            return _coords;
        }

        public List<Vector2Int> GetCoords(Vector2Int origin)
        {
            return _coords.Select(coord => coord + origin).ToList();
        }
    }
}