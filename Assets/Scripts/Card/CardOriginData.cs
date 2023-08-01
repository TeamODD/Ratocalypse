using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardOriginData
    {
        private static CardOriginData _instance;

        private readonly Dictionary<int, CardData> _data;

        private CardOriginData()
        {
            _data = new Dictionary<int, CardData>();
        }

        public static CardOriginData Instance => _instance ??= new CardOriginData();

        public int Count => _data.Count;

        public bool AddData(CardData data)
        {
            if (_data.ContainsKey(data.Id))
            {
                return false;
            }
            _data[data.Id] = data;
            return true;
        }

        public bool AddData(Func<CardData> generator)
        {
            return AddData(generator());
        }

        public bool RemoveData(int id)
        {
            return _data.Remove(id);
        }

        public bool RemoveData(CardData cardData)
        {
            return RemoveData(cardData.Id);
        }

        public bool RemoveDataBy(Func<CardData, bool> filter)
        {
            bool ret = true;
            foreach (CardData data in _data.Values.Where(filter))
            {
                ret &= _data.Remove(data.Id);
            }
            return ret;
        }

        public TCardData GetData<TCardData>(int id) where TCardData : CardData
        {
            if (!_data.ContainsKey(id))
            {
                return null;
            }
            CardData data = _data[id];
            if (data is not TCardData cardData)
            {
                throw new InvalidCastException("Cannot cast");
            }
            return cardData;
        }

        public CardData CreateOriginCard(int id)
        {
            if (!_data.ContainsKey(id))
            {
                throw new KeyNotFoundException("No card data with id " + id + " found");
            }

            return _data[id].CloneOriginCard();
        }
    }
}