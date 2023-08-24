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
using System.Linq;

namespace TeamOdd.Ratocalypse.CardLib.CardDatas.Templates
{
    [CardRegister(typeof(ValueData))]
    public class Blessing : CardData
    {

        public override string GetTitle()
        {
            return "에너지 전송";
        }

        public override string GetDescription()
        {
            return $"범위 내에 있는 모든 아군은 각자 다음 공격시에 데미지를 2배 줍니다.\n다음 라운드에 효과가 사라집니다.\n패링시 성공한 경우에만 발동합니다.";
        }

        private DirectionalMovement CreateMapSelecting(CreatureData caster)
        {
            Pattern pattern = Pattern.GetChessPattern(OriginValueData.rangeType);
            DirectionalMovement movement = new DirectionalMovement(caster, pattern, true,(placement)=>{
                return !Utils.IsEnemy(caster, placement, true);
            });
            return movement;
        }

        public override MapSelecting GetPreview(CreatureData caster)
        {
            return CreateMapSelecting(caster);
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

            castCard.SetTrigger((result, _) =>
            {
                SelectMap.Result selectResult = result as SelectMap.Result;

                TriggerCard triggerCard = new TriggerCard(null, caster, 0, null);
                triggerCard.AddCommand((_) =>
                {
                    return new GetPlacementInRange(CreateMapSelecting(caster));
                });

                triggerCard.AddCommand((result)=>{
                    var getAllPlacementResult = result as GetPlacementInRange.Result;
                    var allies = getAllPlacementResult.placements.Cast<CreatureData>();
                    var chainCommand = new ChainCommand();

                    foreach(var ally in allies)
                    {
                        chainCommand.AddCommand((_)=>{
                            return new SetEffect(ally, "Blessing", null);
                        });
                    }
                    return chainCommand;
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