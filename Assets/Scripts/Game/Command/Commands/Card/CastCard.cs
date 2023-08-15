using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands
{
    public class CastCard : ChainCommand
    {
        private Func<ICommandResult, TriggerCard> _triggerCommandCreator;
        private bool _runTrigger;
        private CardCastData _cardCastData;
        public CastCard(CardCastData cardCastData, bool runTrigger) : base(cardCastData)
        {
            _cardCastData = cardCastData;
            _runTrigger = runTrigger;
        }

        public void SetTrigger(Func<ICommandResult, TriggerCard> triggerCommandCreator)
        {
            _triggerCommandCreator = triggerCommandCreator;
        }

        public override ExecuteResult Execute()
        {
            Command nextCommand = Next(_parm);
            if (nextCommand != null)
            {
                nextCommand.RegisterOnEnd(SetParm);
                return new SubCommand(nextCommand);
            }

            _cardCastData.Caster.TriggerCard();
            var triggerCommand = _triggerCommandCreator(_parm);
            if(_runTrigger)
            {
               return new NextCommand(triggerCommand);
            }
            return new End(triggerCommand);
        }
    }
}