using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.DataCommands;
using UnityEngine;


namespace TeamOdd.Ratocalypse.CardLib.CardDatas.Templates
{
    [CardRegister(typeof(ValueData))]
    public class MoveOrAttack : CardData
    {
        public override CastCard CreateCastCardCommand(CardCastData cardCastData, bool runTrigger)
        {
            CreatureData caster = null;
            CastCard castCard = new CastCard(cardCastData, runTrigger);

            castCard.AddCommand((result) =>
            {
                CardCastData data = result as CardCastData;
                caster = data.Caster;
                var temp = new SelectMap((OriginValueData as ValueData).RangeType, data.Caster);
                return temp;
            });

            castCard.SetTrigger((result) =>
            {
                SelectMap.Result selectResult = result as SelectMap.Result;
                int damage = 0;
                if (selectResult.SelectedPlacement != null)
                {
                    damage = 10;
                }

                TriggerCard triggerCard = new TriggerCard(null, damage, selectResult.SelectedCoord);
                triggerCard.AddCommand((_) =>
                {
                    if (selectResult.SelectedPlacement == null)
                    {
                        return new Move(caster, selectResult.SelectedCoord);
                    }
                    else
                    {
                        return new Attack(caster, selectResult.SelectedPlacement as IDamageable,triggerCard.CalculateFinalDamage());
                    }
                });
                return triggerCard;
            });

            return castCard;
        }

        public class ValueData : CardValueData
        {
            public ChessRangeType RangeType;
        }
    }
}