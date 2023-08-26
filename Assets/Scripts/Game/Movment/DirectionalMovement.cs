using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
namespace TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib
{
    public class DirectionalMovement : MapSelecting
    {
        private Placement _target;
        private Pattern _pattern;
        
        private Func<Placement, bool> _filter;
        private bool _bypassPlacement = false;

        private bool _containTile;
        private bool _containPlacement;

        public DirectionalMovement(bool containTile, bool containPlacement, Placement target, Pattern pattern,Func<Placement, bool> filter = null)
        {
            _containTile = containTile;
            _containPlacement = containPlacement;
            _filter = filter;
            _pattern = pattern;
            _target = target;
        }

        public DirectionalMovement(bool containTile, bool containPlacement,Placement target, Pattern pattern, bool bypassPlacement,Func<Placement, bool> filter = null)
        {
            _containTile = containTile;
            _filter = filter;
            _pattern = pattern;
            _target = target;
            _bypassPlacement = bypassPlacement;
        }

        public override void Calculate(MapData mapData)
        {
            var analyzer = new MapAnalyzer(mapData);
            CoordCandidates = new ShapedCoordList(_target.Shape);

            HashSet<Placement> placements = new HashSet<Placement>();
            var patternCalculations = _pattern.Calculate(mapData.Size, _target.Coord, _target.Shape);

            foreach (var calculation in patternCalculations)
            {
                while (calculation.MoveNext())
                {
                    var coords = calculation.Current;
                    if (!analyzer.CheckAllIn(coords, (_, placement) => placement == null || placement == _target))
                    {
                        analyzer.WhereIn(coords, (placement) => placement != _target)
                                 .ForEach((placement) => placements.Add(placement));
                        if(!_bypassPlacement)
                        {
                            break;
                        }
                    }
                    CoordCandidates.Add(coords[0]);
                }
            }
            PlacementCanditates = placements.ToList();
            if (_filter != null)
            {
                PlacementCanditates = PlacementCanditates.Where(_filter).ToList();
            }

            if (!_containTile)
            {
                CoordCandidates.Coords = new List<Vector2Int>();
            }
            if (!_containPlacement)
            {
                PlacementCanditates = new List<Placement>();
            }
        }
    }
}