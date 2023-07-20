using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Cat
{
    public class Cat : Creature
    {
        [SerializeField]
        protected CatData _catData;

        public override void Initiate(Placement placement, IMapCoord mapCoord)
        {
            _catData = (CatData)placement; 
            base.Initiate(placement, mapCoord);
        }
        
        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
        }

        protected override void OnCoordChanged(Vector2Int coord)
        {
            
        }
    }
}