using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TeamOdd.Ratocalypse.Card
{
    public class CardDataCollection : IEnumerable<CardData>
    {
        protected readonly List<CardData> _cardDataList;

        public CardDataCollection(params CardData[] cardDataItems)
        {
            _cardDataList = cardDataItems.ToList();
        }

        public int Count { get => _cardDataList.Count; }

        public CardData GetCard(int index)
        {
            return _cardDataList[index];
        }

        public void AddCard(CardData cardData)
        {
            _cardDataList.Add(cardData);
        }

        public void AddCards(params CardData[] cardDataItems)
        {
            foreach (CardData cardData in cardDataItems)
            {
                AddCard(cardData);
            }
        }
        public void RemoveCard(int index)
        {
            _cardDataList.RemoveAt(index);
        }

        public void RemoveCards(ISet<int> indices)
        {
            SortedSet<int> sortedIndices = new SortedSet<int>(indices);
            foreach (int i in sortedIndices.Reverse())
            {
                _cardDataList.RemoveAt(i);
            }
        }

        public void Clear()
        {
            _cardDataList.Clear();
        }

        public IEnumerator<CardData> GetEnumerator()
        {
            return _cardDataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}