using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;


namespace TeamOdd.Ratocalypse.CreatureLib.Cat
{
    public class Cat : Creature
    {

        [SerializeField]
        protected CatData _catData;

        public void Initiate(CatData catData, IMapCoord mapCoord)
        {
            _catData = catData; 
            base.Initiate(catData, mapCoord);
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