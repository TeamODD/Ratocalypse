using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace TeamOdd.Ratocalypse.Object
{
    public class ObjectController : MonoBehaviour
    {
        public Animator anim;

        [SerializeField] float _Speed = 10.0f;

        public enum ObjectState
        {
            Move,
            Attact,
            Hit,
            Idle,
            Die
        }
        [SerializeField] ObjectState _State = ObjectState.Idle;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch (_State)
            {
                case ObjectState.Idle:
                    UpdateIdle();
                    //
                    break;
                case ObjectState.Move:
                    UpdateMove();
                    //
                    break;
                case ObjectState.Hit:
                    //
                    break;
                case ObjectState.Die:
                    //
                    break;
                case ObjectState.Attact:
                    //
                    break;
            }
        }

        void UpdateIdle()
        {
            //애니메이션 작동
            anim.Play("Idle");
        }

        void UpdateMove()
        {
            //애니메이션 작동
            anim.Play("Move");
        }
    }
}
