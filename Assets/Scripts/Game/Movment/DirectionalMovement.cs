using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
namespace TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib
{
    public class DirectionalMovement
    {
        private MapData _mapData;
        private MapAnalyzer _analyzer;
        private Placement _target;
        private Pattern _pattern;

        public DirectionalMovement(Placement target, MapData mapData, Pattern pattern)
        {
            _mapData = mapData;
            _pattern = pattern;
            _target = target;
            _analyzer = new MapAnalyzer(mapData);
        }

        public Selection CreateSelection(Action<ShapedCoordList,int> tileSelectionCallback, Action<Placement> placementSelectionCallback = null)
        {
            ShapedCoordList candidates = new ShapedCoordList(_target.Shape);
            Dictionary<Vector2Int, int> selectionMap = new Dictionary<Vector2Int, int>();
            HashSet<Placement> placements = new HashSet<Placement>();
            var calculations = _pattern.Calculate(_mapData.Size, _target.Coord, _target.Shape);

            foreach (var calculation in calculations)
            {
                while (calculation.MoveNext())
                {

                    var coords = calculation.Current;
                    if (!_analyzer.CheckAllIn(coords, (_, placement) => placement == null || placement == _target))
                    {
                        if (placementSelectionCallback!=null)
                        {
                            _analyzer.WhereIn(coords, (placement) => placement != _target)
                                     .ForEach((placement) => placements.Add(placement));
                        }
                        break;
                    }

                    candidates.Add(coords[0]);

                    foreach (Vector2Int coord in coords)
                    {
                        if (!selectionMap.ContainsKey(coord))
                        {
                            int candidateIndex = candidates.Count - 1;
                            selectionMap.Add(coord, candidateIndex);
                        }
                    }
                }
            }
            Selection selection = new Selection(candidates, selectionMap, tileSelectionCallback);
            if(placementSelectionCallback!=null)
            {
                selection.SetPlacementSelection(placements.ToList(), placementSelectionCallback);
            }
            return selection;
        }

    }
}