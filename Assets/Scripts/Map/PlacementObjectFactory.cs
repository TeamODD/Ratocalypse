using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    public class PlacementObjectFactory : MonoBehaviour
    {
        [SerializeField]
        private Map _map;
        [SerializeField]
        private Transform _parent;

        public PlacementObject Create(Placement placement,PlacementObject prefab)
        {
            PlacementObject created = Instantiate(prefab, _parent);
            created.Initiate(placement, _map);
            return created;
        }
    }
}