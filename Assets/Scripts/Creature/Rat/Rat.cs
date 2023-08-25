using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using System;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    [RequireComponent(typeof(RatAnimation))]
    public class Rat : Creature
    {

        [SerializeField]
        protected RatData _ratData;
        
        private RatAnimation _ratAnimation;

        private void Awake() 
        {
            _ratAnimation = GetComponent<RatAnimation>();
        }

        public override void Initialize(Placement placement, IMapCoord mapCoord)
        {
            _ratData = (RatData)placement; 
            base.Initialize(placement, mapCoord);
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
        }

        protected override void OnCoordChanged(Vector2Int coord)
        {
            transform.DOMove(_mapCoord.GetTileWorldPosition(coord), 0.2f);
        }

        protected override void OnHpReduced(int hp)
        {
            
        }

        protected override void OnAnimationEvent(object parm, string name, Action[] callbacks)
        {
            if(name == "Attack")
            {
                _ratAnimation.AttackMotion(callbacks);
            }
            else if(name == "Hit")
            {
                transform.DORotate(new Vector3(0, 0, 10), 0.2f).SetLoops(2, LoopType.Yoyo).OnKill(() => {
                    if(callbacks!=null&&callbacks.Length > 0)
                    {
                        callbacks[0]?.Invoke();
                    }
                });
            }
        }
    }
}