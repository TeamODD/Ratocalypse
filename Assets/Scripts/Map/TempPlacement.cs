using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class TempPlacement : Placement
    {
        public TempPlacement(MapData mapData, Vector2Int coord, Shape shape = null) : base(mapData, coord, shape)
        {
        }
    }

}