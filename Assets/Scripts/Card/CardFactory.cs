
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardFactory:MonoBehaviour
    {
        [SerializeField]
        private Card _prefab;

        public Card Create(CardData cardData, Transform parent)
        {
            Card card = Instantiate(_prefab);
            card.transform.SetParent(parent);
            card.Initialize(cardData);
            return card;
        }
    }
}