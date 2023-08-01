using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.Card;

namespace TeamOdd.Ratocalypse.Deck
{
    public class UndrawnCards : CardDataCollection
    {
        public UndrawnCards()
        {

        }

        public void Shuffle()
        {
            _cardDatas = _cardDatas.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        public CardData Draw()
        {
            CardData cardData = _cardDatas[0];
            _cardDatas.RemoveAt(0);
            return cardData;
        }
    }
}