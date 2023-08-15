using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using UnityEngine;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands
{
    public class TriggerCard : ChainCommand, ICommandResult
    {
        public float DamageMultiplier = 1f;
        private int _damage = 0;
        public int Damage { get; private set;} = 0;
        public Vector2Int? Destenation { get; private set; } = null;

        public TriggerCard(ICommandResult parm, int damage = 0, Vector2Int ?destenation = null) : base(parm)
        {
            Damage = damage;
            Destenation = destenation;
        }

        public int CalculateFinalDamage()
        {
            return (int)(Damage * DamageMultiplier);
        }

        public override ExecuteResult Execute()
        {
            Command nextCommand = Next(_parm);
            if(nextCommand != null)
            {
                nextCommand.RegisterOnEnd(SetParm);
                return new SubCommand(nextCommand);
            }
            else
            {
                return new End(_parm);
            }
        }
    }
}