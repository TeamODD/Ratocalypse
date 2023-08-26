using System.Collections.Generic;
using UnityEngine;
using TeamOdd.Ratocalypse.MapLib;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using TeamOdd.Ratocalypse.CreatureLib.Rat;

namespace TeamOdd.Ratocalypse.CreatureLib.Cat
{
    public class CatFactory : PlacementObjectFactory
    {
        [SerializeField]
        private Cat _catPrefab;
        [SerializeField]
        private Vector3 _catUIOffsets;
        [SerializeField]
        private CreatureUI _creatureUIPrefab;
        [SerializeField]
        private Transform _creatureUIParent;

        public Cat Create(CatData catData)
        {
            Cat created = Create(catData, _catPrefab) as Cat;
            var creatureUI = Instantiate(_creatureUIPrefab, _creatureUIParent);
            creatureUI.AttachUi(created.transform, _catUIOffsets, catData);
            return created;
        }
    }
}