using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardState : IState
{
    HeldCards heldCards;
    
    Deck discardDeck;

    public DiscardState(string name, string nextStateName, HeldCards heldCards,  Deck discardDeck)
    {
        this.name = name;
        this.nextStateName = nextStateName;
        this.heldCards = heldCards;
        
        this.discardDeck = discardDeck;
    }

    public override void Execute(Card card, SourceDeck source)
    {
        discardDeck.Add(card);
        heldCards.Remove(card);
    }
}
