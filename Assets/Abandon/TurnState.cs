using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : IState
{
    HeldCards heldCards;
    Deck drawDeck;
    Deck discardDeck;
    public TurnState(string name, string nextStateName,HeldCards heldCards,Deck drawDeck,Deck discardDeck)
    {
        this.name = name;
        this.nextStateName = nextStateName;
        this.heldCards = heldCards;
        this.drawDeck = drawDeck;
        this.discardDeck = discardDeck;
    }

    public  void PickCardFromDeck()
    {       
        heldCards.Add(drawDeck.Pop());            
       
    }

    public void PickDiscardedCard()
    {
        heldCards.Add(discardDeck.Pop());
    }

    public void DiscardOneCard(Card card)
    {
        discardDeck.Add(card);
        heldCards.Remove(card);
    }
}
