using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.Obstacle;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.ObstacleLib
{
    public class Obstacle: PlacementObject
    {

        [SerializeField]
        protected ObstacleData _obstacleData;

        public override void Initiate(Placement placement, IMapCoord mapCoord)
        {
            _obstacleData = (ObstacleData)placement;
            base.Initiate(placement, mapCoord);
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
        }
    }
}