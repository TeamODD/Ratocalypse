using UnityEngine;
using UnityEditor;
using System;
using TeamOdd.Ratocalypse.CardLib;
using System.Reflection;

[CustomEditor(typeof(CardDataObject))]
[CanEditMultipleObjects]
public class CardDataObjectEditor : Editor
{
    private CardDataObject _cardDataObject;
    private int _cardTypeindex = 0;

    private void OnEnable()
    {
        _cardDataObject = target as CardDataObject;
    }

    public override void OnInspectorGUI()
    {

        CreateMenu();
        foreach(CardCreateData createData in _cardDataObject.cardCreateDatas)
        {
            if(CreateCardEditor(createData))
            {
                break;
            }
        }
    }

    private void Save()
    {
        _cardDataObject.Save();
        EditorUtility.SetDirty(_cardDataObject);
    }

    public bool CreateCardEditor(CardCreateData createData)
    {
        GUILayout.Label("Card Type: " + createData.CardDataType.Name);

        CreateEditorField(createData);

        var remove = GUILayout.Button("Remove");
        if (remove)
        {
            _cardDataObject.cardCreateDatas.Remove(createData);
            Save();
        }

        EditorGUILayout.Space(20);
        return remove;
    }

    public void CreateEditorField(object target)
    {
        var type = target.GetType();
        var fields = type.GetFields();
        foreach(var field in fields)
        {   
            var oldValue = field.GetValue(target);
            var newValue = CreateField(field.Name, oldValue);
            if(oldValue is CardValueData)
            {
                CreateEditorField(oldValue);
                continue;
            }
            else if(!oldValue.Equals(newValue))
            {
                Save();
            }
            field.SetValue(target, newValue);
        }
    }

    public void CreateMenu()
    {
        var cardTypeNames = _cardDataObject.CardTypes.ConvertAll(x => x.Name).ToArray();
        _cardTypeindex = EditorGUILayout.Popup("Card type to add", _cardTypeindex, cardTypeNames);

        if (GUILayout.Button("Add"))
        {
            _cardDataObject.AddCard(_cardDataObject.CardTypes[_cardTypeindex]);
            Save();
        }

        EditorGUILayout.Space(20);
    }


    public object CreateField(string name, object value)
    {
        if (value is int v)
        {
            return EditorGUILayout.IntField(name, v);
        }
        else if (value is float f)
        {
            return EditorGUILayout.FloatField(name, f);
        }
        else if (value is string s)
        {
            return EditorGUILayout.TextField(name, s);
        }
        else if (value is Vector2 vector2)
        {
            return EditorGUILayout.Vector2Field(name, vector2);
        }
        else if (value is Vector3 vector3)
        {
            return EditorGUILayout.Vector3Field(name, vector3);
        }
        else if (value is Vector4 vector4)
        {
            return EditorGUILayout.Vector4Field(name, vector4);
        }
        else if (value is Texture2D texture2D)
        {
            return EditorGUILayout.ObjectField(name, texture2D, typeof(Texture2D), false);
        }
        else if (value is Enum e)
        {
            return EditorGUILayout.EnumPopup(name, e);
        }
        else
        {
            return value;
        }
    }
}