using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib
{
    public interface ICardSelector : ISelector<List<int>>
    {
        public void SetTarget(DeckData deckData);
        public DeckData GetTarget();
    }
}