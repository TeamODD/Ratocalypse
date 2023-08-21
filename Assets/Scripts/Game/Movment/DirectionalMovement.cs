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

        private ShapedCoordList _coordCandidates;
        private List<Placement> _placementCanditates;


        public DirectionalMovement(Placement target, MapData mapData, Pattern pattern)
        {
            _mapData = mapData;
            _pattern = pattern;
            _target = target;
            _analyzer = new MapAnalyzer(mapData);
        }

        public void Calculate()
        {
            _coordCandidates = new ShapedCoordList(_target.Shape);

            HashSet<Placement> placements = new HashSet<Placement>();
            var patternCalculations = _pattern.Calculate(_mapData.Size, _target.Coord, _target.Shape);

            foreach (var calculation in patternCalculations)
            {
                while (calculation.MoveNext())
                {
                    var coords = calculation.Current;
                    if (!_analyzer.CheckAllIn(coords, (_, placement) => placement == null || placement == _target))
                    {
                        _analyzer.WhereIn(coords, (placement) => placement != _target)
                                 .ForEach((placement) => placements.Add(placement));
                        break;
                    }
                    _coordCandidates.Add(coords[0]);
                }
            }
            _placementCanditates = placements.ToList();
        }

        public Selection<ShapedCoordList> CreateCoordSelection(Action<Vector2Int> onSelect, Action onCancel = null)
        {
            var selection = new Selection<ShapedCoordList>(_coordCandidates,
            (index)=>{
                onSelect(_coordCandidates.GetCoord(index));
            }, onCancel);
            return selection;
        }

        public Selection<List<Placement>> CreatePlacementSelection(Action<Placement> onSelect, Action onCancel = null)
        {
            var selection = new Selection<List<Placement>>(_placementCanditates,
            (index)=>{
                onSelect(_placementCanditates[index]);
            }, onCancel);
            return selection;
        }

    }
}