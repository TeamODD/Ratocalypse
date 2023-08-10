using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    [System.Serializable]
    public class CreatureData : Placement, IDamageable, IAttackable
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

        public List<string> StatusEffectList;
        [field: ReadOnly, SerializeField]
        public DeckData DeckData { get; private set; }

        public CreatureData(float maxHp, int maxStamina, MapData mapData, Vector2Int coord, Shape shape, List<(int,CardDataValue)> deck) : base(mapData, coord, shape)
        {
            MaxHp = maxHp;
            MaxStamina = maxStamina;
            DeckData = new DeckData(deck);
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

        public void ReduceStamina(int amount)
        {
            Stamina -= amount;
        }

        public void Attack(IDamageable target, float damage)
        {
            target.ReduceHp(damage);
            OnAttack.Invoke(target, damage);
        }


        public bool CheckCastable()
        {
            return DeckData.HasCastableCard(Stamina);
        }

        public List<(int index, CardData cardData)> GetCastableCards()
        {
            return DeckData.GetCastableCards(Stamina);
        }

        public CardData CastCard(int index)
        {
            return DeckData.CastCard(index);
        }

        public void CancelCast()
        {
            DeckData.CancelCast();
        }

        public void TriggerCard()
        {
            int cost = DeckData.TriggerCard();
            ReduceStamina(cost);
        }


    }
}