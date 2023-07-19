using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;


namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    public class Rat : Creature
    {

        [SerializeField]
        protected RatData _ratData;

        public void Initiate(RatData ratData, IMapCoord mapCoord)
        {
            _ratData = ratData; 
            base.Initiate(ratData, mapCoord);
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