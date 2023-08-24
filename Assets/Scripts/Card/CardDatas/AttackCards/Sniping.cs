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
    public class Sniping : CardData
    {

        public override string GetTitle()
        {
            return "스나이핑";
        }

        public override string GetDescription()
        {
            return $"내 손에 있는 카드의 수 만큼 피해를 줍니다.";
        }


        private DirectionalMovement CreateMovement(CreatureData caster)
        {
            Pattern pattern = Pattern.GetChessPattern(OriginValueData.rangeType);
            DirectionalMovement movement = new DirectionalMovement(caster, pattern, (placement)=>{
                return Utils.IsEnemy(caster, placement);
            });
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
                var temp = new SelectMap(CreateMovement(caster), data.Caster, true, true);
                return temp;
            });

            

            castCard.SetTrigger((result, _) =>
            {
                int damage = caster.DeckData.GetHandCards().Count + 1;
                SelectMap.Result selectResult = result as SelectMap.Result;

                TriggerCard triggerCard = new TriggerCard(null, caster, damage, selectResult.SelectedCoord);
                triggerCard.AddCommand((_) =>
                {
                    if (selectResult.SelectedCoord != null)
                    {
                        return new Move(caster, selectResult.SelectedCoord.Value);
                    }
                    if (selectResult.SelectedPlacement != null)
                    {
                        IDamageable target = selectResult.SelectedPlacement as IDamageable;
                        return new Attack(caster, target, triggerCard.CalculateFinalDamage());
                    }
                    return null;
                });

                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {

        }
    }
}