using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using DG.Tweening;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;
using UnityEngine.Events;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    public class Creature : PlacementObject
    {

        [SerializeField]
        protected CreatureData _creatrueData;

        public void Initiate(CreatureData creatureData, IMapCoord mapCoord)
        {
            _creatrueData = creatureData;
            base.Initiate(creatureData, mapCoord);
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();

            _creatrueData.OnHpReduced.AddListener(OnHpReduced);
            _creatrueData.OnHpRestored.AddListener(OnHpRestored);
            _creatrueData.OnDie.AddListener(OnDie);
        }

        protected override void OnCoordChanged(Vector2Int coord)
        {
            
        }

        protected virtual void OnHpReduced(float hp)
        {
            
        }

        protected virtual void OnHpRestored(float hp)
        {
            
        }

        protected virtual void OnDie()
        {
            
        }
    }
}