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
        private int _maxStep = 0;

        public Pattern(List<Vector2Int> deltas, int step = 1, int maxStep = 0)
        {
            _deltas = deltas;
            _step = step;
            _maxStep = maxStep;
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
            int maxStep = _maxStep == 0 ? maxLoop : _maxStep + 1;

            maxLoop = Mathf.Min(maxStep, maxLoop);

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


        public static Pattern GetChessPattern(ChessRangeType rangeType)
        {
            List<Vector2Int> deltas = new List<Vector2Int>();
            int maxStep = 0;
            switch (rangeType)
            {
                case ChessRangeType.Knight:
                    deltas.Add(new Vector2Int(1, 2));
                    deltas.Add(new Vector2Int(2, 1));
                    deltas.Add(new Vector2Int(2, -1));
                    deltas.Add(new Vector2Int(1, -2));
                    deltas.Add(new Vector2Int(-1, -2));
                    deltas.Add(new Vector2Int(-2, -1));
                    deltas.Add(new Vector2Int(-2, 1));
                    deltas.Add(new Vector2Int(-1, 2));
                    maxStep = 1;
                    break;
                case ChessRangeType.King:
                    deltas.Add(new Vector2Int(1, 1));
                    deltas.Add(new Vector2Int(1, 0));
                    deltas.Add(new Vector2Int(1, -1));
                    deltas.Add(new Vector2Int(0, -1));
                    deltas.Add(new Vector2Int(-1, -1));
                    deltas.Add(new Vector2Int(-1, 0));
                    deltas.Add(new Vector2Int(-1, 1));
                    deltas.Add(new Vector2Int(0, 1));
                    maxStep = 1;
                    break;
                case ChessRangeType.Queen:
                    deltas.Add(new Vector2Int(1, 1));
                    deltas.Add(new Vector2Int(1, 0));
                    deltas.Add(new Vector2Int(1, -1));
                    deltas.Add(new Vector2Int(0, -1));
                    deltas.Add(new Vector2Int(-1, -1));
                    deltas.Add(new Vector2Int(-1, 0));
                    deltas.Add(new Vector2Int(-1, 1));
                    deltas.Add(new Vector2Int(0, 1));
                    break;
                case ChessRangeType.Rook:
                    deltas.Add(new Vector2Int(1, 0));
                    deltas.Add(new Vector2Int(0, -1));
                    deltas.Add(new Vector2Int(-1, 0));
                    deltas.Add(new Vector2Int(0, 1));
                    break;
                case ChessRangeType.Bishop:
                    deltas.Add(new Vector2Int(1, 1));
                    deltas.Add(new Vector2Int(1, -1));
                    deltas.Add(new Vector2Int(-1, -1));
                    deltas.Add(new Vector2Int(-1, 1));
                    break;
                default:
                    break;
            }
            return  new Pattern(deltas, 1, maxStep);
        }
    }
}