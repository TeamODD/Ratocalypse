using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib
{
    public class Movement
    {
        public enum State
        {
            None,
            Selecting,
            Moved,
        }
        private MapData _mapData;
        private MapAnalyzer _analyzer;
        private Placement _target;
        private Pattern _pattern;

        public State CurrentState { get; private set; } = State.None;
        private List<List<Vector2Int>> _candidates = new List<List<Vector2Int>>();
        private Dictionary<Vector2Int, int> _selectionMap = new Dictionary<Vector2Int, int>();

        public Movement(Placement target, MapData mapData, Pattern pattern)
        {
            _mapData = mapData;
            _pattern = pattern;
            _target = target;
            _analyzer = new MapAnalyzer(mapData);
        }

        public (List<List<Vector2Int>>, Dictionary<Vector2Int, int>) StartSelect()
        {
            if (CurrentState == State.Selecting)
            {
                throw new System.Exception("Already Started Selecting");
            }
            _candidates.Clear();
            _selectionMap.Clear();

            var calculations = _pattern.Calculate(_mapData.Size, _target.Coord, _target.Shape);

            foreach (var calculation in calculations)
            {
                while (calculation.MoveNext())
                {
                    
                    var coords = calculation.Current;
                    if (!_analyzer.CheckAllIn(coords, (_, placement) => placement == null || placement == _target))
                    {
                        break;
                    }
                    
                    _candidates.Add(coords);

                    foreach (Vector2Int coord in coords)
                    {
                        if (!_selectionMap.ContainsKey(coord))
                        {
                            int candidateIndex = _candidates.Count - 1;
                            _selectionMap.Add(coord, candidateIndex);
                        }
                    }
                }
            }

            CurrentState = State.Selecting;
            return (_candidates, _selectionMap);
        }

        public void Select(int index)
        {
            _target.SetCoord(_candidates[index][0]);
            CurrentState = State.Moved;
        }

    }
}