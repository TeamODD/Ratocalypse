using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands;
using UnityEngine;
using System.Collections.Generic;
using static TeamOdd.Ratocalypse.MapLib.MapData;
using TeamOdd.Ratocalypse.MapLib.GameLib.MovemnetLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.CreatureLib.Cat;

namespace TeamOdd.Ratocalypse.CardLib.CardDatas.Templates
{
    [CardRegister(typeof(ValueData))]
    public class Cursed : CardData, IVolatileCard
    {

        public override string GetTitle()
        {
            return "저주 : 신벌";
        }

        public override string GetDescription()
        {
            return $"효과가 없습니다.\n사용하거나 다음 라운드가 되면 사라집니다.";
        }

        public override MapSelecting GetPreview(CreatureData caster)
        {
            return null;
        }

        public override CastCard CreateCastCardCommand(CardCastData cardCastData, bool runTrigger)
        {
            CreatureData caster = null;
            CastCard castCard = new CastCard(cardCastData, runTrigger);
            castCard.AddCommand((result) =>
            {
                CardCastData data = result as CardCastData;
                caster = data.Caster;
                return null;
            });

            castCard.SetTrigger((_, _) =>
            {
                TriggerCard triggerCard = new TriggerCard(null, caster, 0, null);
                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {

        }
    }
}