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
    public class Rage : CardData
    {

        public override string GetTitle()
        {
            return "짓이긴 분노";
        }

        public override string GetDescription()
        {
            return $"내 방어도 만큼 근력을 올립니다.";
        }

        private DirectionalMovement CreateMovement(CreatureData caster)
        {
            Pattern pattern = Pattern.GetChessPattern(OriginValueData.rangeType);
            DirectionalMovement movement = new DirectionalMovement(caster, pattern);
            return movement;
        }

        public override MapSelecting GetPreview(CreatureData caster)
        {
            return CreateMovement(caster);
        }

        public override CastCard CreateCastCardCommand(CardCastData cardCastData, bool runTrigger)
        {
            CreatureData caster = null;
            CastCard castCard = new CastCard(cardCastData, runTrigger);

            castCard.AddCommand((result) =>
            {
                CardCastData data = result as CardCastData;
                caster = data.Caster;
                var temp = new SelectMap(CreateMovement(caster), data.Caster, false, true);
                return temp;
            });

             
            castCard.SetTrigger((result, _) =>
            {
                int amount = caster.Armor;
                SelectMap.Result selectResult = result as SelectMap.Result;

                TriggerCard triggerCard = new TriggerCard(null, caster, 0, selectResult.SelectedCoord);
                triggerCard.AddCommand((_) =>
                {
                    return new GainStrength(caster, amount);
                });
                if (selectResult.SelectedCoord != null)
                {
                    triggerCard.AddCommand((_) =>
                    {
                        return new Move(caster, selectResult.SelectedCoord.Value);
                    });
                }

                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {
            
        }
    }
}