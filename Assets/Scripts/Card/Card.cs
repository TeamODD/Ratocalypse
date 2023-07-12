using UnityEngine;

namespace TeamOdd.Ratocalypse.Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private long _id;

        [SerializeField]
        private CardDataValue _cardDataValue;

        public void Awake()
        {
            // TODO: implement this...
        }

        public void Start()
        {
            // TODO: implement this...
        }

        public void Update()
        {
            // TODO: implement this...
        }

        public void Initiate(CardData cardData, bool clone = false)
        {
            if (clone)
            {
                Initiate(cardData.CardDataId, cardData.DataValue.Clone());
            }
            else
            {
                Initiate(cardData.CardDataId, cardData.DataValue);
            }
        }

        public void Initiate(long id, CardDataValue cardDataValue)
        {
            _id = id;
            _cardDataValue = cardDataValue;
        }
    }
}

