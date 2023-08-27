using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands
{
    public class CastCard : ChainCommand
    {
        private Func<ICommandResult, CreatureData, TriggerCard> _triggerCommandCreator;
        private bool _runTrigger;
        private CardCastData _cardCastData;
        public CastCard(CardCastData cardCastData, bool runTrigger) : base(cardCastData)
        {
            _cardCastData = cardCastData;
            _runTrigger = runTrigger;
        }

        public void SetTrigger(Func<ICommandResult, CreatureData, TriggerCard> triggerCommandCreator)
        {
            _triggerCommandCreator = triggerCommandCreator;
        }

        public override ExecuteResult Execute()
        {
            if(!_cardCastData.Caster.IsAlive())
            {
                return new End();
            }
            if(_cardCastData.Caster.CastCardData == null)
            {
                if(_runTrigger)
                {
                    return new NextCommand(new SelectAndCastCard(_cardCastData.Caster, true));
                }
                var cancelResult = new Result();
                cancelResult.Cancel = true;
                return new End(cancelResult);
            }



            Command nextCommand = Next(_parm);
            
            if (nextCommand != null)    
            {
                nextCommand.RegisterOnEnd(SetParm);
                return new SubCommand(nextCommand);
            }


            _cardCastData.Caster.TriggerCard();
            var triggerCommand = _triggerCommandCreator(_parm, _cardCastData.Caster);
            var result = new Result();
            result.TriggerCard = triggerCommand;
            if(_runTrigger)
            {
               return new NextCommand(triggerCommand);
            }
            return new End(result);
        }

        public class Result:ICommandResult
        {
            public bool Cancel = false;
            public TriggerCard TriggerCard;
        }
    }
}