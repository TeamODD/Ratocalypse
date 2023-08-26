using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib
{
    public class PlacementObjectFactory : MonoBehaviour
    {
        [SerializeField]
        protected Map _map;
        [SerializeField]
        protected Transform _parent;

        public PlacementObject Create(Placement placement,PlacementObject prefab)
        {
            PlacementObject created = Instantiate(prefab, _parent);
            created.Initialize(placement, _map);
            return created;
        }
    }
}