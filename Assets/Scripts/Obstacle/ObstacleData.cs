using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.Obstacle
{
    public class ObstacleData : Placement, IDamageable
    {

        public float MaxHp { get; private set; }
        public float Hp { get; private set; }

        public UnityEvent OnDie{get; private set;}
        public UnityEvent<float> OnHpReduced{ get; private set; }
        public UnityEvent<float> OnHpRestored{ get; private set; }

        public ObstacleData(float maxHp, MapData mapData, Vector2Int coord,List<Vector2Int> shape):base(mapData, coord, shape)
        {
            MaxHp = maxHp;
            Hp = MaxHp;

            OnDie = new UnityEvent();
            OnHpReduced = new UnityEvent<float>();
            OnHpRestored = new UnityEvent<float>();
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