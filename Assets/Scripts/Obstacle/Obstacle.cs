using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.Obstacle;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;

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

        protected override void OnCoordChanged(Vector2Int coord)
        {
            transform.DOMove(_mapCoord.GetTileWorldPosition(coord), 0.2f);
        }
    }
}