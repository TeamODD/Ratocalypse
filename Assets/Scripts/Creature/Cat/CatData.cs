using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Cat
{
    [System.Serializable]
    public class CatData : CreatureData
    {
        static private Shape _shape = new Shape(2, 2);

        public CatData(float maxHp, int maxStamina, MapData mapData, Vector2Int coord)
        : base(maxHp, maxStamina, mapData, coord, _shape)
        {

        }

    }
}