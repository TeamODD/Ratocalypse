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
    public class FrontalAssault : CardData
    {

        public override string GetTitle()
        {
            return "전방 돌격!";
        }

        public override string GetDescription()
        {
            return $"피해를 {GetAttackDamage()}을 적에게 줍니다.\n 자신에게도 피해를 {GetGainDamage()}을 줍니다.";
        }

        private int GetAttackDamage()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.AttackDamage + gameValueData.AttackDamage; 
        }

        private int GetGainDamage()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.GainDamage + gameValueData.GainDamage; 
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

            int attackDamage = GetAttackDamage();
            int gainDamage = GetGainDamage();

            castCard.SetTrigger((result, _) =>
            {
                SelectMap.Result selectResult = result as SelectMap.Result;

                TriggerCard triggerCard = new TriggerCard(null, caster, attackDamage, selectResult.SelectedCoord);

                if (selectResult.SelectedCoord != null)
                {
                    triggerCard.AddCommand((_) =>
                    {
                        return new Move(caster, selectResult.SelectedCoord.Value);
                    });
                }
                else if (selectResult.SelectedPlacement != null)
                {   IDamageable target = selectResult.SelectedPlacement as IDamageable;
                    triggerCard.AddCommand((_) =>
                    {
                        return new Attack(caster, target, triggerCard.CalculateFinalDamage());
                    });
                }
                triggerCard.AddCommand((_) =>
                {
                    return new Damage(caster, gainDamage);
                });

                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {
            public int AttackDamage = 0;
            public int GainDamage = 0;
        }
    }
}