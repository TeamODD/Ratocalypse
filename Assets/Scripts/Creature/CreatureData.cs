using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.StatusEffectn;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Creature
{
    public class CreatureData : MonoBehaviour
    {
        [SerializeField] private float hp;
        [SerializeField] private int stamina;
        public List<StatusEffect> OnStatusEffect = new List<StatusEffect>();
        public void HpControal(float value)
        {
            hp += value;
        }
        public void Stemina(int value)
        {
            if (stamina > value)
            {
                return;
            }
            stamina -= value;
            //Card.Action 함수 들어갈 곳
        }
        
    }
}