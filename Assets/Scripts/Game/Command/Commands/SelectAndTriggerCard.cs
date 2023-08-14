using UnityEngine;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class SelectAndTriggerCard : Command
    {
        private CreatureData _creature;

        public SelectAndTriggerCard(CreatureData creature)
        {
            _creature = creature;
        }

        private CardCastData _cardCastData;
        private TriggerCard _triggerCard;

        public override ExecuteResult Execute()
        {
            if (_cardCastData == null)
            {
                SelectCastCard selectCommand = new SelectCastCard(_creature);
                selectCommand.RegisterOnEnd((result) =>
                {
                    _cardCastData = (CardCastData)result;
                });
                return new SubCommand(selectCommand);
            }
            else if (_triggerCard == null)
            {
                CastCard castCommand = new CastCard(_cardCastData);
                castCommand.RegisterOnEnd((result) =>
                {
                    _triggerCard = (result as CastCard.Result).TriggerCommand;
                });
                return new SubCommand(castCommand);
            }
            else
            {
                return new NextCommand(_triggerCard);
            }
        }

        public class Result : ICommandResult
        {
            public TriggerCard TriggerCard = null;
        }
    }
}