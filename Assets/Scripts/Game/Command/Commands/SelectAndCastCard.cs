using UnityEngine;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using TeamOdd.Ratocalypse.CardLib.Command;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class SelectAndCastCard : Command
    {
        private CreatureData _creature;

        public SelectAndCastCard(CreatureData creature)
        {
            _creature = creature;
        }

        private CardCastData? _cardCastData;
    
        public override ExecuteResult Execute()
        {
            if(_cardCastData == null)
            {
                SelectCastCard selectCommand = new SelectCastCard(_creature);
                selectCommand.RegisterOnEnd((result)=>{
                    _cardCastData = (CardCastData)result;
                });
                return new SubCommand(selectCommand);
            }
            CastCard castCommand = new CastCard(_cardCastData.Value);
            return NextCommand(castCommand);
        }
    }
}