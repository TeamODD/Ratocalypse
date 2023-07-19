using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Cat
{
    public class CatData : CreatureData
    {
        static private List<Vector2Int> _shape = new List<Vector2Int>{
            new Vector2Int(0,0),new Vector2Int(0,1),new Vector2Int(1,0),new Vector2Int(1,1)};
        
        public CatData(float maxHp, int maxStamina, MapData mapData, Vector2Int coord)
        : base(maxHp, maxStamina, mapData, coord, _shape)
        {
            
        }

    }
}