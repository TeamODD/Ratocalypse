using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
namespace TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib
{
    public class AllRange : MapSelecting
    {
        private Func<Placement, bool> _filter;

        public AllRange(Func<Placement, bool> filter = null)
        {
            _filter = filter;
        }


        public override void Calculate(MapData mapData)
        {
            CoordCandidates = null;
            PlacementCanditates = mapData.GetPlacements();
            if(_filter!=null)
            {
                PlacementCanditates = PlacementCanditates.Where(_filter).ToList();
            }
        }
    }
}