using TeamOdd.Ratocalypse.CreatureLib;

namespace TeamOdd.Ratocalypse.Card
{
    public class AttackCardCommand : ICardCommand
    {
        private readonly Creature _attacker;
        private readonly Creature _target;

        public AttackCardCommand(Creature attacker, Creature target)
        {
            _attacker = attacker;
            _target = target;
        }

        public virtual void Execute()
        {
            // TODO: implement this...
        }
    }
}