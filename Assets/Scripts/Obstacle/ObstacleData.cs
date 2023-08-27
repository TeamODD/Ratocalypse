using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.Obstacle
{
    [System.Serializable]
    public class ObstacleData : Placement, IDamageable
    {
        [SerializeField]
        public int MaxHp { get; private set; }
        [SerializeField]
        public int Hp { get; private set; }

        public int Armor { get; private set; }

        public UnityEvent OnDie{get; private set;}
        public UnityEvent<int> OnHpReduced{ get; private set; }
        public UnityEvent<int> OnHpRestored{ get; private set; }

        public UnityEvent<object, string, Action[]> AnimationEvent{get; private set;} = new UnityEvent<object, string, Action[]>();

        public UnityEvent<int> OnArmorIncreased => new UnityEvent<int>();

        public UnityEvent<int> OnArmorReduced => new UnityEvent<int>();

        public UnityEvent<int> OnArmorChanged => new UnityEvent<int>();

        private bool _die = false;

        public ObstacleData(int maxHp, MapData mapData, Vector2Int coord, Shape shape):base(mapData, coord, shape)
        {
            MaxHp = maxHp;
            Hp = MaxHp;

            OnDie = new UnityEvent();
            OnHpReduced = new UnityEvent<int>();
            OnHpRestored = new UnityEvent<int>();
        }

        public void Die()
        {
            _die = true;
            OnDie.Invoke();
        }

        public void ReduceHp(int amount)
        {
            Hp = Mathf.Max(0, Hp - amount);
            OnHpReduced.Invoke(Hp);

        }

        public void RestoreHp(int amount)
        {
            Hp = Mathf.Min(MaxHp, Hp + amount);
            OnHpRestored.Invoke(Hp);
        }

        public bool IsAlive()
        {
            return !_die;
        }

        public void IncreaseArmor(int amount)
        {
            
        }

        public void ReduceArmor(int amount)
        {
            
        }
    }
}