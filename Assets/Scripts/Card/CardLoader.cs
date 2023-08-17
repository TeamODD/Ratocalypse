using System;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardLoader : MonoBehaviour
    {
        private CardOriginData _cardOriginData;
        [SerializeField]
        private string _path;

        [SerializeField]
        private List<CardDataObject> _cardDataObjects = new List<CardDataObject>();

        private void Awake()
        {
            _cardOriginData = CardOriginData.Instance;
            foreach (var cardDataObject in _cardDataObjects)
            {
                LoadCards(cardDataObject.cardCreateDatas);
            }
        }

        private void AddCard(CardData data)
        {
            _cardOriginData.AddData(data);
        }

        private void LoadCards(List<CardCreateData> cardCreateDatas)
        {
            foreach (var cardCreateData in cardCreateDatas)
            {
                var cardDataType = cardCreateData.CardDataType;
                var texture = LoadTexture(cardCreateData.TextureName);
                var id = cardCreateData.Id;
                var valueData = cardCreateData.OriginValueData; 
                var cardData = CardData.CreateCardData(cardDataType, id, texture, valueData);
                AddCard(cardData);
            }
        }

        private Texture2D LoadTexture(string name)
        {
            return Resources.Load<Texture2D>(_path + "/" + name);
        }
    }
}
