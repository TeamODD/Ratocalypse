using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using System.Linq;
namespace TeamOdd.Ratocalypse.DeckLib
{
    [System.Serializable]
    public class HandData : CardDataCollection
    {
        public int GetMinCost()
        {
            return _cardDatas.Min(x => x.GetCost());
        }
    }
}
