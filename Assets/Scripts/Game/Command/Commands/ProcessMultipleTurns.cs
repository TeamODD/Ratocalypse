using UnityEngine;
using UnityEngine.Events;
using TeamOdd.Ratocalypse.CreatureLib;
using static TeamOdd.Ratocalypse.CardLib.Cards.Templates.MoveOrAttackCardData;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using static TeamOdd.Ratocalypse.MapLib.GameLib.ExecuteResult;
using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TeamOdd.Ratocalypse.CreatureLib.Rat;
using System.Linq;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.Commands
{
    public class ProcessMultipleTurns : Command
    {
        private List<CreatureData> _creatureDatas;

        public ProcessMultipleTurns(List<CreatureData> creatureDatas)
        {
            _creatureDatas = creatureDatas;
        }

        private bool _processed = false;

        private CastJointAttack _ratsAttack;
        private CastJointAttack _catsAttack;

        private CastJointAttack.Result _ratsAttackResult;
        private CastJointAttack.Result _catsAttackResult;

        public override ExecuteResult Execute()
        {
            if(!_processed)
            {
                Process();
            }

            if(_ratsAttack != null&& _ratsAttackResult == null)
            {
                _ratsAttack.Execute();
                _ratsAttack.RegisterOnEnd((result) =>
                {
                    _ratsAttackResult = (CastJointAttack.Result)result;
                });
                return new SubCommand(_ratsAttack);
            }

            if(_catsAttack != null && _catsAttackResult == null)
            {
                _catsAttack.Execute();
                _catsAttack.RegisterOnEnd((result) =>
                {
                    _catsAttackResult = (CastJointAttack.Result)result;
                });
                return new SubCommand(_catsAttack);
            }

            if(_ratsAttackResult!=null && _ratsAttackResult.TriggerCards.Count == 0)
            {
                _ratsAttackResult = null;
            }

            if(_catsAttackResult!=null &&_catsAttackResult.TriggerCards.Count == 0)
            {
                _catsAttackResult = null;
            }

            if(_ratsAttackResult != null && _catsAttackResult != null)
            {
                var parying = new ParryAttacks(_catsAttackResult, _ratsAttackResult);
                return new NextCommand(parying);
            }
            else if(_ratsAttackResult == null && _catsAttackResult != null)
            {
                var commands = _catsAttackResult.TriggerCards.Cast<Command>().ToList();
                return new NextCommands(commands);
            }
            else if(_ratsAttackResult != null && _catsAttackResult == null)
            {
                var commands = _ratsAttackResult.TriggerCards.Cast<Command>().ToList();
                return new NextCommands(commands);
            }
            return new End();

        }


        private void Process()
        {
            var cats = new List<CreatureData>();
            var rats = new List<CreatureData>();

            foreach(var creatureData in _creatureDatas)
            {
                if(creatureData is CatData catData)
                {
                    cats.Add(catData);
                }
                else if(creatureData is RatData ratData)
                {
                    rats.Add(creatureData);
                }
            }

            if(cats.Count > 0)
            {
                _catsAttack = new CastJointAttack(cats);
            }
            if(rats.Count > 0)
            {
                _ratsAttack = new CastJointAttack(rats);
            }

            _processed = true;
        } 
    }
}