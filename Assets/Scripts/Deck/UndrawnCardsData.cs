using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.CardLib;

namespace TeamOdd.Ratocalypse.DeckLib
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
            if(_cardDatas.Count == 0)
            {
                return null;
            }
            CardData cardData = _cardDatas[0];
            _cardDatas.RemoveAt(0);
            return cardData;
        }
    }
}