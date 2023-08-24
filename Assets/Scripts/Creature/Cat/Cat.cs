using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using System;

namespace TeamOdd.Ratocalypse.CreatureLib.Cat
{
    public class Cat : Creature
    {
        [SerializeField]
        protected CatData _catData;

        public override void Initialize(Placement placement, IMapCoord mapCoord)
        {
            _catData = (CatData)placement; 
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

        protected override void OnAnimationEvent(object parm, string name, Action[] callbacks)
        {
            if(name == "Attack")
            {
                transform.DORotate(new Vector3(0, 0, 30), 1f).OnComplete(() => {
                    if(callbacks!=null&&callbacks.Length > 0)
                    {
                        callbacks[0]?.Invoke();
                    }
                    transform.DORotate(new Vector3(0, 0, 0), 1f).OnComplete(() => {
                        if(callbacks!=null&&callbacks.Length > 1)
                        {
                            callbacks[1]?.Invoke();
                        }
                    });
                });
                
            }
            else if(name == "Hit")
            {
                transform.DORotate(new Vector3(0, 0, 10), 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
                    if(callbacks!=null&&callbacks.Length > 0)
                    {
                        callbacks[0]?.Invoke();
                    }
                });
            }
        }
    }
}