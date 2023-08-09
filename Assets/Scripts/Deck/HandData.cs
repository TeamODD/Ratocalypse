using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using System.Linq;
namespace TeamOdd.Ratocalypse.DeckLib
{
    [System.Serializable]
    public class HandData : CardDataCollection
    {
        public int MaxCount { get; private set; } = 20;

        public int GetMinCost()
        {
            return _cardDatas.Min(x => x.GetCost());
        }
    }
}
