using UnityEngine;
using DG.Tweening;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using UnityEngine.EventSystems;

namespace TeamOdd.Ratocalypse.UI
{
    public class RatIconFactory : MonoBehaviour
    {
        [SerializeField]
        private RatIcon _prefab;

        public RatIcon Create(Transform parent,RatData ratData)
        {
            RatIcon created = Instantiate(_prefab, parent);
            created.transform.localPosition = Vector3.zero;
            created.Initialize(ratData);
            return created;
        }
    }
}