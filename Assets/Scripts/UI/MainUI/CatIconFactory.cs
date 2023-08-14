using UnityEngine;
using TeamOdd.Ratocalypse.CreatureLib.Cat;

namespace TeamOdd.Ratocalypse.UI
{
    public class CatIconFactory : MonoBehaviour
    {
        [SerializeField]
        private CatIcon _prefab;

        public CatIcon Create(Transform parent, CatData catData)
        {
            CatIcon created = Instantiate(_prefab, parent);
            created.transform.localPosition = Vector3.zero;
            created.Initialize(catData);
            return created;
        }
    }
}