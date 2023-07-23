using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;

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
            transform.DOMove(_mapCoord.GetTileWorldPosition(coord), 0.2f);
        }

        protected override void OnHpReduced(float hp)
        {
            
        }

        protected override void OnAttack(IDamageable target, float damage)
        {
            _ratAnimation.AttackMotion();
        }
    }
}