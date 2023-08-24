using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using TeamOdd.Ratocalypse.TestScripts;
using System;


namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    [RequireComponent(typeof(AttackAnimationExample))]
    [RequireComponent(typeof(RatPose))]
    public class RatAnimation : MonoBehaviour
    {
        private RatPose _ratPose;
        private AttackAnimationExample _jumpAnimation;

        private void Awake() 
        {
            _jumpAnimation = GetComponent<AttackAnimationExample>();
            _ratPose = GetComponent<RatPose>();
        }

        public void AttackMotion(params Action[] callbacks)
        {
            _jumpAnimation.Jump(()=>{
                _ratPose.SetPose(RatPose.Pose.Attack);
                if(callbacks!=null&&callbacks.Length>0)
                {
                    callbacks[0]?.Invoke();
                }
            },
            ()=>{
                _ratPose.SetPose(RatPose.Pose.Idle);
            },
            ()=>{
                if(callbacks!=null&&callbacks.Length>1)
                {
                    callbacks[1]?.Invoke();
                }
            });
        }
    }
}