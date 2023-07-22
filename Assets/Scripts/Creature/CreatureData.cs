using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    [System.Serializable]
    public class CreatureData : Placement, IDamageable
    {
        [field: ReadOnly, SerializeField]
        public float MaxHp { get; private set; }
        [field: ReadOnly, SerializeField]
        public float Hp { get; private set; }

        [field: ReadOnly, SerializeField]
        public int MaxStamina { get; private set; }
        [field: ReadOnly, SerializeField]
        public int Stamina { get; private set; }

        public UnityEvent<float> OnHpReduced {get; private set;}
        public UnityEvent<float> OnHpRestored {get; private set;}
        public UnityEvent OnDie { get; private set;}
        
        public List<string> StatusEffectList;

        public CreatureData(float maxHp,int maxStamina,MapData mapData, Vector2Int coord,Shape shape):base(mapData, coord, shape)
        {
            MaxHp = maxHp;
            MaxStamina = maxStamina;

            OnHpReduced = new UnityEvent<float>();
            OnHpRestored = new UnityEvent<float>();
            OnDie = new UnityEvent();
            Init();
        }

        public void Init()
        {
            Hp = MaxHp;
            Stamina = MaxStamina;
        }

        public void Die()
        {
            OnDie.Invoke();
        }

        public void ReduceHp(float amount)
        {
            Hp = Mathf.Max(0, Hp - amount);
            OnHpReduced.Invoke(Hp);

            if(Hp <= 0)
            {
                Die();
            }
        }

        public void RestoreHp(float amount)
        {
            Hp = Mathf.Min(MaxHp, Hp + amount);
            OnHpRestored.Invoke(Hp);
        }
    }
}