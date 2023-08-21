using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    [Serializable]
    public partial class  CreatureData : Placement, IDamageable, IAttackable, IAnimatable
    {
        [field: ReadOnly, SerializeField]
        public float MaxHp { get; private set; }
        [field: ReadOnly, SerializeField]
        public float Hp { get; private set; }

        [field: ReadOnly, SerializeField]
        public int MaxStamina { get; private set; }

        [field: ReadOnly, SerializeField]
        public int Stamina { get; private set; }

        public UnityEvent<float> OnHpReduced { get; private set; } = new UnityEvent<float>();
        public UnityEvent<float> OnHpRestored { get; private set; } = new UnityEvent<float>();
        public UnityEvent OnDie { get; private set; } = new UnityEvent();
        public UnityEvent<IDamageable, float> OnAttack { get; private set; } = new UnityEvent<IDamageable, float>();

        public UnityEvent<object, string, Action[]> AnimationEvent{get; private set;} = new UnityEvent<object, string, Action[]>();
        
        public List<string> StatusEffectList;
        
        [field: ReadOnly, SerializeField]
        public DeckData DeckData{ get; private set; }
        private ICardSelector _cardSelector;
        private Selection<List<int>> _currentCardSelection;
        private (int index,CardData cardData) ?_castCardData = null;

        public CreatureData(float maxHp, int maxStamina, MapData mapData,
                            Vector2Int coord, Shape shape, List<int> deck,
                            ICardSelector cardSelector, CardColor cardColor = CardColor.Blue) 
                            :base(mapData, coord, shape)
        {
            MaxHp = maxHp;
            MaxStamina = maxStamina;
            _cardSelector = cardSelector;
            DeckData = new DeckData(deck, cardColor);
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

            if (Hp <= 0)
            {
                Die();
            }
        }

        public void RestoreHp(float amount)
        {
            Hp = Mathf.Min(MaxHp, Hp + amount);
            OnHpRestored.Invoke(Hp);
        }

        public void RestoreAllStamina()
        {
            Stamina = MaxStamina;
        }

        public void ReduceStamina(int amount)
        {
            Stamina -= amount;
        }

        public void Attack(IDamageable target, float damage)
        {
            target.ReduceHp(damage);
            OnAttack.Invoke(target, damage);
        }

        public bool IsAlive()
        {
            return Hp > 0;
        }
    }
}