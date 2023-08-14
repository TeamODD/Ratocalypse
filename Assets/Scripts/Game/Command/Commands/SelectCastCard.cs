using System;
using System.Collections;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using System.Linq;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.CreatureLib.Rat;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class SelectCastCard : Command
    {
        private ICardSelector _ratSelector;
        private ICardSelector _catSelector;

        private CreatureData _creatureData;

        public SelectCastCard(CreatureData creatureData)
        {
            _creatureData = creatureData;
        }

        public override ExecuteResult Execute()
        {
            var (endWait, result) = CreateWait();
            var indicies = _creatureData.GetCastableCardIndices();
            var selection = new Selection<List<int>>(indicies,
            (int index)=>{
                var cardCastData = new CardCastData(_creatureData,index);
                endWait(new End(cardCastData));
            });
            _creatureData.SetCardSlection(selection);
            _creatureData.SelectCard();
            return result;
        }


    }
}