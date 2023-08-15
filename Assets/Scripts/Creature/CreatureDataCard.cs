using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using TeamOdd.Ratocalypse.CardLib.CommandLib;
using TeamOdd.Ratocalypse.CreatureLib.Attributes;
using TeamOdd.Ratocalypse.DeckLib;
using TeamOdd.Ratocalypse.MapLib;
using TeamOdd.Ratocalypse.MapLib.GameLib;
using TeamOdd.Ratocalypse.MapLib.GameLib.Commands.CardCommands;
using TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib;
using UnityEngine;
using UnityEngine.Events;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.CreatureLib
{
    public partial class CreatureData : Placement, IDamageable, IAttackable
    {

        public List<int> GetCastableCardIndices()
        {
            var castableCards = new List<int>();
            var cards = DeckData.GetHandCards();
            for (int i = 0; i < cards.Count; i++)
            {
                CardData cardData = cards[i];
                if (cardData.GetCost() <= Stamina)
                {
                    castableCards.Add(i);
                }
            }
            return castableCards;
        }

        public bool HasCastableCard()
        {
            foreach (CardData cardData in DeckData.GetHandCards())
            {
                if (cardData.GetCost() <= Stamina)
                {
                    return true;
                }
            }
            return false;
        }

        public CastCard CastCard(int index,bool runTrigger)
        {
            CardData castCardData = DeckData.RemoveCardAtFromHand(index);
            _castCardData = (index, castCardData);
            CardCastData cardCastData =  new CardCastData(this, index);
            return castCardData.CreateCastCardCommand(cardCastData, runTrigger);
        }

        public void TriggerCard()
        {
            if (_castCardData == null)
            {
                throw new System.InvalidOperationException("No card is casting");
            }
            var (index, cardData) = _castCardData.Value;
            DeckData.AddCardToTomb(cardData);
            var cost = cardData.GetCost();
            ReduceStamina(cost);
            _castCardData = null;
        }

        public void CancelCast()
        {
            var (index, cardData) = _castCardData.Value;
            DeckData.InsertHandAt(index, cardData);
            _castCardData = null;
        }

        public void SetCardSlection(Selection<List<int>> selection)
        {
            _currentCardSelection = selection;
        }

        public void RemoveSelection()
        {
            _currentCardSelection = null;
        }

        public void SelectCard()
        {
            _cardSelector.SetTarget(DeckData);
            if(_currentCardSelection != null)
            {
                _cardSelector.Select(_currentCardSelection);
            }
        }

    }
}