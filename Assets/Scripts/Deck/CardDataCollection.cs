﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.DeckLib
{
    [System.Serializable]
    public class CardDataCollection : IEnumerable<CardData>
    {
        [SerializeField]
        protected List<CardData> _cardDatas = new List<CardData>();

        public CardDataCollection(params CardData[] cardDataItems)
        {
            _cardDatas = cardDataItems.ToList();
        }

        public int Count { get => _cardDatas.Count; }

        public CardData GetCard(int index)
        {
            return _cardDatas[index];
        }

        public void AddCard(CardData cardData)
        {
            _cardDatas.Add(cardData);
        }

        public void InsertCard(int index, CardData cardData)
        {
            _cardDatas.Insert(index, cardData);
        }

        public void AddCards(params CardData[] cardDataItems)
        {
            foreach (CardData cardData in cardDataItems)
            {
                AddCard(cardData);
            }
        }
        public CardData RemoveCard(int index)
        {
            CardData cardData = _cardDatas[index];
            _cardDatas.RemoveAt(index);
            return cardData;
        }

        public void RemoveCard(CardData cardData)
        {
            _cardDatas.Remove(cardData);
        }


        public void RemoveCards(ISet<int> indices)
        {
            SortedSet<int> sortedIndices = new SortedSet<int>(indices);
            foreach (int i in sortedIndices.Reverse())
            {
                _cardDatas.RemoveAt(i);
            }
        }

        public void Clear()
        {
            _cardDatas.Clear();
        }

        public IEnumerator<CardData> GetEnumerator()
        {
            return _cardDatas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}