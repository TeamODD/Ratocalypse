using System.Collections.Generic;
using System.Linq;

namespace TeamOdd.Ratocalypse.Card
{
    public class Deck
    {
        private List<CardData> _cardList;

        public Deck(params CardData[] cardDatas)
        {
            _cardList = cardDatas.ToList();
        }

        public int Count { get => _cardList.Count; }

        public CardData GetCard(int index)
        {
            return _cardList[index];
        }

        public void AddCard(CardData cardData)
        {
            _cardList.Add(cardData);
        }

        public void RemoveCards(ISet<int> indices)
        {
            SortedSet<int> sortedIndices = new SortedSet<int>(indices);
            foreach (int i in sortedIndices.Reverse())
            {
                _cardList.RemoveAt(i);
            }
        }

        public void RemoveCard(int index)
        {
            _cardList.RemoveAt(index);
        }
    }
}