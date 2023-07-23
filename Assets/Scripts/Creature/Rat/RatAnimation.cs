using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using TeamOdd.Ratocalypse.TestScripts;

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

        public void AttackMotion()
        {
            _jumpAnimation.Jump(()=>{
                _ratPose.SetPose(RatPose.Pose.Attack);
            },
            ()=>{
                _ratPose.SetPose(RatPose.Pose.Idle);
            },
            ()=>{
                Debug.Log("Attack End");
            });
        }
    }
}