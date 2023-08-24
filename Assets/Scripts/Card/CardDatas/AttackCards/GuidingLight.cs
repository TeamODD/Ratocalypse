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
using System;

namespace TeamOdd.Ratocalypse.CardLib.CardDatas.Templates
{
    [CardRegister(typeof(ValueData))]
    public class GuidingLight : CardData
    {

        public override string GetTitle()
        {
            return "인도하는 빛";
        }

        public override string GetDescription()
        {
            return $"범위 안 적군에게 피해를 {GetDamage()}줍니다.\n이후 범위 안 모든 아군의 체력을 {GetDamage()} 회복시킵니다";
        }

        private int GetDamage()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.Damage + gameValueData.Damage;
        }

        private DirectionalMovement CreateMapSelecting(CreatureData caster, bool enemy = false)
        {
            Pattern pattern = Pattern.GetChessPattern(OriginValueData.rangeType);
            Func<Placement, bool> filter = enemy ? (Placement placement)=>{
                return Utils.IsEnemy(caster, placement, false);
            } : null;
            DirectionalMovement movement = new DirectionalMovement(caster, pattern, true, filter);
            return movement;
        }

        public override MapSelecting GetPreview(CreatureData caster)
        {
            return CreateMapSelecting(caster, true);
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
                int damage = GetDamage();
                SelectMap.Result selectResult = result as SelectMap.Result;

                TriggerCard triggerCard = new TriggerCard(null, caster, damage, null);
                triggerCard.AddCommand((_) =>
                {
                    return new GetPlacementInRange(CreateMapSelecting(caster));
                });
                List<CreatureData> enemies = new List<CreatureData>();
                List<CreatureData> allies = new List<CreatureData>();

                triggerCard.AddCommand((result)=>{
                    var getAllPlacementResult = result as GetPlacementInRange.Result;
                    var creatureDatas = getAllPlacementResult.placements.Cast<CreatureData>();
                    var chainCommand = new ChainCommand();

                    foreach(var creatureData in creatureDatas)
                    {
                        if(Utils.IsEnemy(caster, creatureData, false))
                        {
                            enemies.Add(creatureData);
                        }
                        else if(!Utils.IsEnemy(caster, creatureData, true))
                        {
                            allies.Add(creatureData);
                        }
                    }

                    foreach(var enemy in enemies)
                    {
                        chainCommand.AddCommand((_)=>{
                            return new Attack(caster,enemy, triggerCard.CalculateFinalDamage());
                        });
                    }

                    return chainCommand;
                });

                triggerCard.AddCommand((result)=>{
                    var chainCommand = new ChainCommand();
                    foreach(var ally in allies)
                    {
                        chainCommand.AddCommand((_)=>{
                            return new Heal(ally, damage);
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
            public int Damage = 0;
        }
    }
}