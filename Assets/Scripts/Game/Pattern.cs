using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public class Pattern
    {
        private MapData _mapData;
        private List<Vector2Int> _deltas;
        private int _step = 1;

        public Pattern(List<Vector2Int> deltas, int step = 1)
        {
            _deltas = deltas;
            _step = step;
        }


        public List<IEnumerator<List<Vector2Int>>> Calculate(Vector2Int size, Vector2Int origin, Shape shape)
        {
            List<IEnumerator<List<Vector2Int>>> result = new List<IEnumerator<List<Vector2Int>>>();
            foreach (Vector2Int delta in _deltas)
            {
                result.Add(CalculateDelta(delta, size, origin, shape));
            }
            return result;
        }


        private IEnumerator<List<Vector2Int>> CalculateDelta(Vector2Int delta, Vector2Int size, Vector2Int origin, Shape shape)
        {
            int maxLoop = Mathf.Max(size.y, size.x);
            
            Vector2Int start = origin;

            for (int loop = 1; loop < maxLoop; loop++)
            {
                List<Vector2Int> outputCoords = new List<Vector2Int>();
                foreach (Vector2Int point in shape)
                {
                    Vector2Int newCoord = start + (delta * _step * loop) + point;
                    if (newCoord.x >= 0 && newCoord.x < size.x && newCoord.y >= 0 && newCoord.y < size.y)
                    {
                        outputCoords.Add(newCoord);
                    }
                    else
                    {
                        yield break;
                    }
                }

                yield return outputCoords;

            }
        }
    }
}