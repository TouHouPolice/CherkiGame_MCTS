using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class DiscardState : CherkiMachineState
{
    CardsInHand mCards;
    
    Deck discardDeck;

    public DiscardState(string name, string nextStateName, CardsInHand mCards,  Deck discardDeck ,string mTurn)
    {
        this.name = name;
        this.nextStateName = nextStateName;
        this.mCards = mCards;
        
        this.discardDeck = discardDeck;
        this.turn = mTurn;
    }

    public override void Execute(Card card )
    {
        discardDeck.Add(card);
        discardDeck.LeftShiftElement();
        mCards.Remove(card);
        mCards.LeftShiftElement();
        mCards.Sort();
    }
}
*/