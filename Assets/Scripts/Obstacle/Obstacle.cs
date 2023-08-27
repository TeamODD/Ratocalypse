using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.Obstacle;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using UnityEngine.Events;
using System;

namespace TeamOdd.Ratocalypse.ObstacleLib
{
    public class Obstacle: PlacementObject, IAnimatable
    {

        [SerializeField]
        protected ObstacleData _obstacleData;

        public UnityEvent<object, string, Action[]> AnimationEvent{get; private set;} = new UnityEvent<object, string, Action[]>();

        public override void Initialize(Placement placement, IMapCoord mapCoord)
        {
            _obstacleData = (ObstacleData)placement;
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

        protected void OnAnimationEvent(object parm, string name, Action[] callbacks)
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
                transform.DORotate(new Vector3(0, 0, 10), 0.2f).SetLoops(2, LoopType.Yoyo).OnKill(() => {
                    if(callbacks!=null&&callbacks.Length > 0)
                    {
                        callbacks[0]?.Invoke();
                    }
                });
            }
            else if(name=="Die")
            {
                transform.DOScale(0, 0.2f).OnComplete(()=>{
                    if(callbacks!=null&&callbacks.Length > 0)
                    {
                        callbacks[0]?.Invoke();
                    }
                });
            }
        }
    }
}