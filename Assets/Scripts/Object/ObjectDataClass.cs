using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.StatusEffect;

namespace TeamOdd.Ratocalypse.Object
{
    public class ObjectDataClass : MonoBehaviour
    {
        [SerializeField] private float _Hp;
        [SerializeField] private int _Stemina;
        [SerializeField] private List<StatusEffectClass> _OnStatusEffect;

        private void Awake()
        {
            _Hp = 100;
            _Stemina = 3;
            _OnStatusEffect = new List<StatusEffectClass>();
        }
        public void HpControal(float value)
        {
            _Hp += value;
        }
        public void Stemina(int value)
        {
            //value = card.cost 
            if (_Stemina > value)
            {
                return; 
            }
            _Stemina -= value;
            //Card.Action 함수 들어갈 곳

        }
        void StatusEffectAtion()
        {
            // 턴시작마자 작동-

            float Value =0;
            //구현한 사태이상효과 분류 및 작동 방식 임의로 구현
            if (_OnStatusEffect.Count < 0)
            {
                return;
            }
            foreach(StatusEffectClass Status in _OnStatusEffect)
            {
                switch(Status.Type) 
                {
                    case "EX_Fire": Value -= 2; break;
                    case "EX_Poison": Value -= 0.5f; break;
                }
            }

        }
    }

}
