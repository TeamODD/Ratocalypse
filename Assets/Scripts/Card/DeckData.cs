using System.Collections.Generic;

namespace TeamOdd.Ratocalypse.Card
{
    public class DeckData
    {
        private readonly List<long> _cardDataIds;
        private readonly List<CardDataValue> _cardDataValues;

        public DeckData(params CardData[] cardDataItems)
        {
            foreach (CardData cardData in cardDataItems)
            {
                _cardDataIds.Add(cardData.CardDataId);
                _cardDataValues.Add(cardData.DataValue);
            }
        }

        public int Count { get => _cardDataIds.Count; }

        public long GetCardId(int index)
        {
            return _cardDataIds[index];
        }

        public CardDataValue GetCardDataValue(int index)
        {
            return _cardDataValues[index];
        }

        public void AddCard(CardData cardData)
        {
            _cardDataIds.Add(cardData.CardDataId);
            _cardDataValues.Add(cardData.DataValue);
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
            _cardDataIds.RemoveAt(index);
            _cardDataValues.RemoveAt(index);
        }

        public void RemoveCards(ISet<int> indices)
        {
            SortedSet<int> sortedIndices = new SortedSet<int>(indices);
            foreach (int i in sortedIndices.Reverse())
            {
                _cardDataIds.RemoveAt(i);
                _cardDataValues.RemoveAt(i);
            }
        }

        public void Clear()
        {
            _cardDataIds.Clear();
            _cardDataValues.Clear();
        }

        public IEnumerator<long> GetIdEnumerator()
        {
            return _cardDataIds.GetEnumerator();
        }

        public IEnumerator<CardDataValue> GetCardDataValueEnumerator()
        {
            return _cardDataValues.GetEnumerator();
        }
    }
}