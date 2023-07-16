using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
namespace TeamOdd.Ratocalypse.Creature
{
    public class CreatureFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _creaturePrefab;
        [SerializeField]
        private Map _map;
        [SerializeField]
        private Transform _parent;

        public void CreateCreature(Vector2Int coord)
        {
            var creature = Instantiate(_creaturePrefab, _parent).GetComponent<Creature>();
            creature.Initiate(new CreatureData(), _map.MapData, _map, coord);
        }
    }
}