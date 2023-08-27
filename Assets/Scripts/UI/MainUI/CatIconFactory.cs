using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.TestScripts;

namespace TeamOdd.Ratocalypse.UI
{
    public class CatIconFactory : MonoBehaviour
    {
        [SerializeField]
        private CatIcon _prefab;

        public CatIcon Create(Transform parent, CatData catData, TileSelector tileSelector)
        {
            CatIcon created = Instantiate(_prefab, parent);
            created.transform.localPosition = Vector3.zero;
            created.Initialize(catData, tileSelector);
            return created;
        }
    }
}