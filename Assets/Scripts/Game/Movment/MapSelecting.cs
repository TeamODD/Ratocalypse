using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib
{
    public class MapSelecting
    {        
        public ShapedCoordList CoordCandidates {get; protected set;}
        public List<Placement> PlacementCanditates {get; protected set;}

        public MapSelecting()
        {

        }

        public MapSelecting(ShapedCoordList coordCandidates, List<Placement> placementCanditates)
        {
            CoordCandidates = coordCandidates;
            PlacementCanditates = placementCanditates;
        }

        public virtual void Calculate(MapData mapData)
        {
            
        }

        public Selection<ShapedCoordList> CreateCoordSelection(Action<Vector2Int?> onSelect, Action onCancel = null)
        {
            var selection = new Selection<ShapedCoordList>(CoordCandidates,
            (index)=>{
                if(index == -1)
                {
                    onSelect(null);
                    return;
                }
                onSelect(CoordCandidates.GetCoord(index));
            }, onCancel);
            return selection;
        }

        public Selection<List<Placement>> CreatePlacementSelection(Action<Placement> onSelect, Action onCancel = null)
        {
            var selection = new Selection<List<Placement>>(PlacementCanditates,
            (index)=>{
                if(index == -1)
                {
                    onSelect(null);
                    return;
                }
                onSelect(PlacementCanditates[index]);
            }, onCancel);
            return selection;
        }
    }
}