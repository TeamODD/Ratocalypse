using System;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    public class CardCreateData
    {
        public int Id;
        public string TextureName;
        public Type CardDataType;
        public CardValueData OriginValueData;
        
        public CardCreateData(int id, string textureName, Type cardDataType, CardValueData originValueData)
        {
            Id = id;
            TextureName = textureName;
            CardDataType = cardDataType;
            OriginValueData = originValueData;
        }

    }
}