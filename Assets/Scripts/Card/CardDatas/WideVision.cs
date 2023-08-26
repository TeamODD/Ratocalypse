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
    public class WideVision : CardData
    {

        public override string GetTitle()
        {
            return "넓은 시야";
        }

        public override string GetDescription()
        {
            return $"이동 후: 모든 아군에게 \"지원 : 넓은 시야\" 카드를 줍니다.";
        }

        private DirectionalMovement CreateMovement(CreatureData caster)
        {
            Pattern pattern = Pattern.GetChessPattern(OriginValueData.rangeType);
            DirectionalMovement movement = new DirectionalMovement(true,false,caster, pattern);
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
                Vector2Int prevCoord = caster.Coord;
                SelectMap.Result selectResult = result as SelectMap.Result;

                TriggerCard triggerCard = new TriggerCard(null, caster, 0, selectResult.SelectedCoord);

                if (selectResult.SelectedCoord != null)
                {
                    triggerCard.AddCommand((_) =>
                    {
                        return new Move(caster, selectResult.SelectedCoord.Value);
                    });
                }

                triggerCard.AddCommand((_) =>
                {
                    return new GetAllPlacements((placement)=>!Utils.IsEnemy(placement, caster, true));
                });

                triggerCard.AddCommand((result)=>{
                    var getAllPlacementResult = result as GetAllPlacements.Result;
                    var allies = getAllPlacementResult.placements.Cast<CreatureData>();
                    var chainCommand = new ChainCommand();
                    var cardId = (OriginValueData as ValueData).CardId;
                    foreach(var ally in allies)
                    {
                        chainCommand.AddCommand((_)=>{
                            return new InsertCard(ally, cardId);
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
            public int CardId;
        }
    }
}