
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private Transform _createPosition;

        public CardView Create(CardData cardData, Transform parent)
        {
            CardView card = CreateDummy(parent);
            card.View(cardData);
            return card;
        }

        public CardView CreateDummy(Transform parent)
        {
            CardView card = Instantiate(_prefab).GetComponent<CardView>();
            card.transform.SetParent(parent);
            card.transform.position = _createPosition.position;
            card.transform.localRotation = Quaternion.Euler(0, 0, 0);
            return card;
        }
    }
}