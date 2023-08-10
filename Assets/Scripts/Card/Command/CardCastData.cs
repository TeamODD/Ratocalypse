using System;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;

namespace TeamOdd.Ratocalypse.CardLib.CommandLib
{
    public struct CardCastData : ICommandResult
    {
        public int CardIndex;
        public CreatureData Caster;

        public CardCastData(CreatureData caster, int index) : this()
        {
            Caster = caster;
            CardIndex = index;
        }
    }
}
