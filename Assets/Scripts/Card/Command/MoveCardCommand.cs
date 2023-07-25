using UnityEngine;

namespace TeamOdd.Ratocalypse.Card
{
    public class MoveCardCommand : ICardCommand
    {
        protected readonly Vector2Int _start;
        protected readonly Vector2Int _dest;

        public MoveCardCommand(Vector2Int start, Vector2Int dest)
        {
            _start = start;
            _dest = dest;
        }

        public virtual void Execute()
        {
            // TODO: implement this...
        }
    }
}