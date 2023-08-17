using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.DataCommands;
using UnityEngine;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;

namespace TeamOdd.Ratocalypse.CardLib.Cards.Templates
{
    [CardRegister(typeof(DataValue))]
    public class MoveOnly : CardData
    {
        public MoveOnly(Texture2D texture, int cardDataId, CardDataValue originDataValue, int cardType)
        : base(cardDataId, originDataValue, texture, cardType)
        {
            DeckDataValue = new DataValue();
        }


        public override CardData CloneOriginCard()
        {
            CardData cloned = new MoveOnly(Texture, _cardDataId, OriginDataValue, CardType);
            return cloned;
        }

        public override CastCard CreateCastCardCommand(CardCastData cardCastData, bool runTrigger)
        {
            CreatureData caster = null;
            CastCard castCard = new CastCard(cardCastData, runTrigger);

            castCard.AddCommand((result) =>
            {
                CardCastData data = result as CardCastData;
                caster = data.Caster;
                var temp = new SelectMap((OriginDataValue as DataValue).a, data.Caster);
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

        [SerializeField]
        public class DataValue : CardDataValue
        {
            public MoveOrAttackRangeType a;
            public int Test;
            public DataValue() : base(0)
            {
                a = MoveOrAttackRangeType.None;
            }

            public DataValue(int cost, MoveOrAttackRangeType rangeType) : base(cost)
            {
                a = rangeType;
            }

            public override CardDataValue Clone()
            {
                return new DataValue(Cost, a);
            }
        }
    }
}