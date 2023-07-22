using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    [System.Serializable]
    public class RatData : CreatureData
    {
        static private Shape _shape = new Shape(1, 1);

        public RatData(float maxHp, int maxStamina, MapData mapData, Vector2Int coord)
        : base(maxHp, maxStamina, mapData, coord, _shape)
        {

        }

    }
}