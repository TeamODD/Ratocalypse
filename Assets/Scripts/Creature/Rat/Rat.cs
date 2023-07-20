using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    public class Rat : Creature
    {

        [SerializeField]
        protected RatData _ratData;

        public override void Initiate(Placement placement, IMapCoord mapCoord)
        {
            _ratData = (RatData)placement; 
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