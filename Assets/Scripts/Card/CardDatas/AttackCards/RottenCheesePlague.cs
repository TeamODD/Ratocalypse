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
    public class RottenCheesePlague : CardData
    {

        public override string GetTitle()
        {
            return "썩은 치즈 역병";
        }

        public override string GetDescription()
        {
            return $"독을 {GetAmount()} 부여합니다.\n이후 {GetDamage()}의 피해로 {GetCount()}번 공격합니다.";
        }

        private int GetAmount()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.Amount + gameValueData.Amount; 
        }

        private int GetDamage()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.Damage + gameValueData.Damage; 
        }

        private int GetCount()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.Count + gameValueData.Count;
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
            SelectMap.Result selectResult = null;
            castCard.AddCommand((result)=>{
                if(selectResult.SelectedPlacement != null)
                {
                    selectResult = result as SelectMap.Result;
                    var target = selectResult.SelectedPlacement as CreatureData;
                    return new StackPoison(target, GetAmount());
                }
                return null;
            });


            castCard.SetTrigger((_, _) =>
            {
                int count = GetCount();
                int damage =  GetDamage() * count;
                TriggerCard triggerCard = new TriggerCard(null, caster, damage, selectResult.SelectedCoord);

                if (selectResult.SelectedCoord != null)
                {
                    triggerCard.AddCommand((_) =>
                    {
                        return new Move(caster, selectResult.SelectedCoord.Value);
                    });

                }
                else if (selectResult.SelectedPlacement != null)
                {   
                    IDamageable target = selectResult.SelectedPlacement as IDamageable;
                    for(int i = 0;i<count;i++)
                    {
                        triggerCard.AddCommand((_) =>
                        {
                            return new Attack(caster, target, triggerCard.CalculateFinalDamage() / count);
                        });
                    }
                }

                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {
            public int Amount = 0;
            public int Damage = 0;
            public int Count = 0;
        }
    }
}