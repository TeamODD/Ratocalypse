using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using System.Linq;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;

namespace TeamOdd.Ratocalypse.GameLib.Commands.GameSequenceCommands
{
    public class ProcessMultipleTurns : Command
    {
        private List<CreatureData> _creatureDatas;

        private List<CreatureData> _cats = new List<CreatureData>();
        private List<CreatureData> _rats = new List<CreatureData>();

        private CastJointAttack.Result _ratsAttackResult;
        private CastJointAttack.Result _catsAttackResult;

        public ProcessMultipleTurns(List<CreatureData> creatureDatas)
        {
            _creatureDatas = creatureDatas;
            DivideCreatures();
        }

        private ChainCommand CreateParryingChain(CastJointAttack catsAttack, CastJointAttack ratsAttack)
        {
            var parryingChain = new ChainCommand();
            List<TriggerCard> ratTriggers = null;
            parryingChain.AddCommand((_) =>
            {
                return ratsAttack;
            });
            parryingChain.AddCommand((result) =>
            {
                ratTriggers = (result as CastJointAttack.Result).TriggerCards;
                return catsAttack;
            });
            parryingChain.AddCommand((result) =>
            {
                var catTriggers = (result as CastJointAttack.Result).TriggerCards;
                return new ParryAttacks(catTriggers, ratTriggers);
            });
            return parryingChain;
        }

        public override ExecuteResult Execute()
        {

            if (_cats.Count != 0 && _rats.Count != 0)
            {
                var catsAttack = new CastJointAttack(_cats, false);
                var ratsAttack = new CastJointAttack(_rats, false);
                var parrying = CreateParryingChain(catsAttack, ratsAttack);
                return new NextCommand(parrying);
            }
            var creatureList = _cats.Count != 0 ? _cats : _rats;
            return new NextCommand(new CastJointAttack(creatureList, true)); 
        }


        private void DivideCreatures()
        {
            foreach (var creatureData in _creatureDatas)
            {
                if (creatureData is CatData)
                {
                    _cats.Add(creatureData);
                }
                else if (creatureData is RatData)
                {
                    _rats.Add(creatureData);
                }
            }
        }
    }
}