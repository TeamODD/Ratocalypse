using System.Collections.Generic;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Creature
{
    public class CreatureData
    {
        [SerializeField]
        public float Hp { get; private set; }

        [SerializeField]
        public int Stamina { get; private set; }

        public List<string> StatusEffectList;

        public void SetHp(int value)
        {
            Hp = value;
        }
        public void SetStamina(int value)
        {
            Stamina = value;
        }

    }
}

