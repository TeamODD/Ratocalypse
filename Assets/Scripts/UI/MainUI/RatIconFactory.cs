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

        public RatIcon Create(Transform parent, RatData ratData, Texture2D headIcon)
        {
            RatIcon created = Instantiate(_prefab, parent);
            created.transform.localPosition = Vector3.zero;
            created.Initialize(ratData, headIcon);
            return created;
        }
    }
}