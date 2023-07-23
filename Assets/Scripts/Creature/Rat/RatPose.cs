using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using DG.Tweening;
using TeamOdd.Ratocalypse.TestScripts;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    public class RatPose : MonoBehaviour
    {
        public enum Pose
        {
            Idle,
            Attack
        }

        [SerializeField]
        private GameObject _idlePose;
        [SerializeField]
        private GameObject _attackPose;

        private AttackAnimationExample _jumpAnimation;
        private void Awake() 
        {
            _jumpAnimation = GetComponent<AttackAnimationExample>();
        }

        public void SetPose(Pose pose)
        {
            switch(pose)
            {
                case Pose.Idle:
                    _idlePose.SetActive(true);
                    _attackPose.SetActive(false);
                    break;
                case Pose.Attack:
                    _idlePose.SetActive(false);
                    _attackPose.SetActive(true);
                    break;
            }
        }
    }
}