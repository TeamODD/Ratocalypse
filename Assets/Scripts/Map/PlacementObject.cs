using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using DG.Tweening;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class PlacementObject : MonoBehaviour
    {

        [SerializeField]
        protected Placement _placement;

        public Vector2Int Coord => _placement.Coord;
        protected IMapCoord _mapCoord;

        public virtual void Initiate(Placement placement, IMapCoord mapCoord)
        {
            _placement = placement;
            _mapCoord = mapCoord;
            RegisterCallbacks();
        }

        protected virtual void RegisterCallbacks()
        {
            _placement.OnCoordChanged.AddListener(OnCoordChanged);
        }

        protected virtual void OnCoordChanged(Vector2Int coord)
        {
            
        }
    }
}