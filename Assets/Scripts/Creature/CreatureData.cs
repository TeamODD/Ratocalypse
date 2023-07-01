using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.StatusEffectn;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Object
{
    public class CreatureData : MonoBehaviour
    {
        [SerializeField] private float hp;
        [SerializeField] private int stamina;
        [SerializeField] private List<StatusEffect> onStatusEffect = new List<StatusEffect>();
    }
}
