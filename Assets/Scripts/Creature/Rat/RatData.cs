using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    [System.Serializable]
    public class RatData : CreatureData
    {
        static private Shape _shape = new Shape(1, 1);

        public RatData(int maxHp, int maxStamina,
                       MapData mapData, Vector2Int coord,
                       List<int> deck,
                       ICardSelector cardSelector)
        : base(maxHp, maxStamina, mapData, coord, _shape, deck, cardSelector)
        {
            
        }


    }
}