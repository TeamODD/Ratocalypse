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
        public int MaxHp { get; private set; }
        [field: ReadOnly, SerializeField]
        public int Hp { get; private set; }

        [field: ReadOnly, SerializeField]
        public int Armor { get; private set; }

        [field: ReadOnly, SerializeField]
        public int MaxStamina { get; private set; }

        [field: ReadOnly, SerializeField]
        public int Stamina { get; private set; }

        [field: ReadOnly, SerializeField]
        public int Strength { get; private set; }

        public UnityEvent<int> OnHpReduced { get; private set; } = new UnityEvent<int>();
        public UnityEvent<int> OnHpRestored { get; private set; } = new UnityEvent<int>();
        public UnityEvent<int> OnArmorReduced { get; private set; } = new UnityEvent<int>();
        public UnityEvent<int> OnArmorIncreased { get; private set; } = new UnityEvent<int>();
        public UnityEvent OnDie { get; private set; } = new UnityEvent();
        public UnityEvent<IDamageable, int> OnAttack { get; private set; } = new UnityEvent<IDamageable, int>();

        public UnityEvent<object, string, Action[]> AnimationEvent{get; private set;} = new UnityEvent<object, string, Action[]>();
        
        public List<string> StatusEffectList;
        
        [field: ReadOnly, SerializeField]
        public DeckData DeckData{ get; private set; }
        private ICardSelector _cardSelector;
        private Selection<List<int>> _currentCardSelection;
        private (int index,CardData cardData) ?_castCardData = null;

        public CreatureData(int maxHp, int maxStamina, MapData mapData,
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

        public void ReduceHp(int amount)
        {
            if(Armor > 0)
            {
                var armorAfter = Mathf.Max(0, Armor - amount);
                var defensedAmount = Armor - armorAfter;
                amount -= defensedAmount;

                Armor = armorAfter;
            }

            if(amount <= 0)
            {
                return;
            }

            Hp = Mathf.Max(0, Hp - amount);
            OnHpReduced.Invoke(Hp);

            if (Hp <= 0)
            {
                Die();
            }
        }

        public void IncreaseStrength(int amount)
        {
            Strength += amount;
        }

        public void ReduceStrength(int amount)
        {
            Strength = Mathf.Max(0, Strength - amount);
        }

        public void RestoreHp(int amount)
        {
            Hp = Mathf.Min(MaxHp, Hp + amount);
            OnHpRestored.Invoke(Hp);
        }

        public void IncreaseArmor(int amount)
        {
            Armor += amount;
        }

        public void ReduceArmor(int amount)
        {
            Armor =  Mathf.Max(0,Armor - amount);
        }

        public void RestoreAllStamina()
        {
            Stamina = MaxStamina;
        }

        public void ReduceStamina(int amount)
        {
            Stamina -= amount;
        }

        public void Attack(IDamageable target, int damage)
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