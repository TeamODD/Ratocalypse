
using System;
using UnityEngine;

namespace TeamOdd.Ratocalypse.CardLib
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]

    public class CardRegister : Attribute
    {

        public Type DataType { get; private set; }
        public CardRegister(Type dataType)
        {
            DataType = dataType;
        }
    }
}