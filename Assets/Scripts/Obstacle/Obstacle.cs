using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;


namespace TeamOdd.Ratocalypse.Obstacle
{
    public class Obstacle: PlacementObject
    {

        [SerializeField]
        protected ObstacleData _obstacleData;

        public void Initiate(ObstacleData obstacleData, IMapCoord mapCoord)
        {
            _obstacleData = obstacleData; 
            base.Initiate(obstacleData, mapCoord);

            transform.localPosition = mapCoord.GetTileWorldPosition(Coord);
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
        }
    }
}