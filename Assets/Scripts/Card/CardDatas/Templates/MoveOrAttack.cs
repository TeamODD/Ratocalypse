using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.ActionCommands;
using UnityEngine;


namespace TeamOdd.Ratocalypse.CardLib.CardDatas.Templates
{
    [CardRegister(typeof(ValueData))]
    public class MoveOrAttack : CardData
    {
        private int GetDamage()
        {
            var originValueData = OriginValueData as ValueData;
            var gameValueData = GameValueData as ValueData;
            return originValueData.Damage + gameValueData.Damage; 
        }

        public override CastCard CreateCastCardCommand(CardCastData cardCastData, bool runTrigger)
        {
            CreatureData caster = null;
            CastCard castCard = new CastCard(cardCastData, runTrigger);

            castCard.AddCommand((result) =>
            {
                CardCastData data = result as CardCastData;
                caster = data.Caster;
                var temp = new SelectMap((OriginValueData as ValueData).RangeType, data.Caster, true, true);
                return temp;
            });


            int damage = GetDamage();

            castCard.SetTrigger((result, _) =>
            {
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
                        return new Attack(caster, selectResult.SelectedPlacement as IDamageable, triggerCard.CalculateFinalDamage());
                    }
                    return null;
                });

                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {
            public ChessRangeType RangeType;
            public int Damage;
        }
    }
}