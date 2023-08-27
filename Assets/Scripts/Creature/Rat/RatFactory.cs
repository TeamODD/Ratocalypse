using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using TeamOdd.Ratocalypse.UI;

namespace TeamOdd.Ratocalypse.CreatureLib.Rat
{
    public class RatFactory : PlacementObjectFactory
    {
        [SerializeField]
        private List<Rat> _ratPrefabs;
        [SerializeField]
        private List<Vector3> _ratUIOffsets;
        [SerializeField]
        private List<Texture2D> _headIcons;
        [SerializeField]
        private CreatureUI _creatureUIPrefab;
        [SerializeField]
        private Transform _creatureUIParent;


        public Rat Create(RatData ratData, int ratType)
        {
            var prefab = _ratPrefabs[ratType];
            Rat created = Create(ratData, prefab) as Rat;
            var creatureUI = Instantiate(_creatureUIPrefab, _creatureUIParent);
            creatureUI.AttachUi(created.transform, _ratUIOffsets[ratType], ratData);
            return created;
        }
    }
}