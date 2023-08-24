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
    public class SharpendClaws : CardData
    {

        public override string GetTitle()
        {
            return "발톱 세우기";
        }

        public override string GetDescription()
        {
            return $"범위에 있는 모든 적에게 피해를 {GetDamage()} 줍니다.";
        }

        private int GetDamage()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.Damage + gameValueData.Damage;
        }

        private DirectionalMovement CreateMapSelecting(CreatureData caster)
        {
            Pattern pattern = Pattern.GetChessPattern(OriginValueData.rangeType);
            DirectionalMovement movement = new DirectionalMovement(caster, pattern, true, (placement) =>
            {
                return Utils.IsEnemy(caster, placement);
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
            castCard.AddCommand((_) =>
            {
                return new GetPlacementInRange(CreateMapSelecting(caster));
            });

            int damage = GetDamage();
           
            castCard.SetTrigger((result, _) =>
            {
                var getAllPlacementResult = result as GetPlacementInRange.Result;
                var enemies = getAllPlacementResult.placements.Cast<IDamageable>().ToList();
                int count = enemies.Count();
                damage *= count;
                TriggerCard triggerCard = new TriggerCard(null, caster, damage, null);


                triggerCard.AddCommand((_) =>
                {
                    return new Attack(caster, enemies, damage/count); 
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