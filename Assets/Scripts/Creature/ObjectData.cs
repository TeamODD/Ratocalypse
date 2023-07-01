using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.StatusEffectn;

namespace TeamOdd.Ratocalypse.Object
{
    public class ObjectData : MonoBehaviour
    {
        [SerializeField] private float hp;
        [SerializeField] private int stamina;
        [SerializeField] private List<StatusEffect> onStatusEffect = new List<StatusEffect>();


     
    }
}

