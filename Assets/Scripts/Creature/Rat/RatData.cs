using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    public class RatData : CreatureData
    {
        static private List<Vector2Int> _shape = new List<Vector2Int>{Vector2Int.zero};

        public RatData(float maxHp, int maxStamina, MapData mapData, Vector2Int coord)
        : base(maxHp, maxStamina, mapData, coord, _shape)
        {

        }

    }
}