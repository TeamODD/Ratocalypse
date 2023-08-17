using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "CardDatas", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CardDataObject : ScriptableObject
{
    [SerializeField]
    private string _serializedJson;

    public Dictionary<Type, Type> CardValueTypes { get; private set; } = new Dictionary<Type, Type>();
    public List<Type> CardTypes { get; private set; } = new List<Type>();

    public List<CardCreateData> cardCreateDatas = new List<CardCreateData>();
    private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

    public void OnEnable()
    {
        UpdateCardTypes();
        Load();
    }

    public void UpdateCardTypes()
    {
        CardTypes.Clear();
        CardValueTypes.Clear();
        var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

        foreach (var type in types)
        {
            var cardRegisters = type.GetCustomAttributes(typeof(CardRegister), true);
            if (cardRegisters.Length == 0)
            {
                continue;
            }
            var cardRegister = cardRegisters.First() as CardRegister;
            CardValueTypes.Add(type, cardRegister.DataType);
            CardTypes.Add(type);
        }
    }

    public void AddCard(Type cardType)
    {
        var cardDataValueType = CardValueTypes[cardType];
        var cardValue = Activator.CreateInstance(cardDataValueType) as CardDataValue;
        cardCreateDatas.Add(new CardCreateData(0, "texture", cardType, cardValue));
    }

    public void Save()
    {
        _serializedJson = JsonConvert.SerializeObject(cardCreateDatas, _jsonSettings);
    }

    public void Load()
    {
        if(_serializedJson == "")
        {
            return;
        }
        cardCreateDatas = JsonConvert.DeserializeObject<List<CardCreateData>>(_serializedJson, _jsonSettings);
    }

}