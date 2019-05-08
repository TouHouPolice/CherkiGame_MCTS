using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawState : IState
{
    HeldCards heldCards;
    Deck drawDeck;
    Deck discardDeck;

    public DrawState(string name, string nextStateName, HeldCards heldCards, Deck drawDeck, Deck discardDeck)
    {
        this.name = name;
        this.nextStateName = nextStateName;
        this.heldCards = heldCards;
        this.drawDeck = drawDeck;
        this.discardDeck = discardDeck;
    }
    public override void Execute(Card card,SourceDeck source)
    {
        if (source == SourceDeck.DrawDeck)
        {
            heldCards.Add(drawDeck.Pop());
        }
        else if (source == SourceDeck.DiscardDeck)
        {
            heldCards.Add(discardDeck.Pop());
        }
        else
        {
            Debug.Log("Error, not from any deck");
        }
    }

    


}
