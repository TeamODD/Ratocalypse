using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using DG.Tweening;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    public class Creature : PlacementObject
    {
        [SerializeField]
        protected CreatureData _creatrueData;

        public override void Initialize(Placement placement, IMapCoord mapCoord)
        {
            _creatrueData = (CreatureData)placement;
            base.Initialize(placement, mapCoord);
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();

            _creatrueData.OnHpReduced.AddListener(OnHpReduced);
            _creatrueData.OnHpRestored.AddListener(OnHpRestored);
            _creatrueData.OnDie.AddListener(OnDie);
            _creatrueData.AnimationEvent.AddListener(OnAnimationEvent);
        }

        protected override void OnCoordChanged(Vector2Int coord)
        {
            
        }

        protected virtual void OnHpReduced(int hp)
        {
            
        }

        protected virtual void OnHpRestored(int hp)
        {
            
        }

        protected virtual void OnDie()
        {
            
        }

        protected virtual void OnAnimationEvent(object parm, string name, Action[] callbacks)
        {
            
        }
    }
}