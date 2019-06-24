using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class DrawState : CherkiMachineState
{
    CardsInHand mCards;
    Deck drawDeck;
    Deck discardDeck;

    public DrawState(string name, string nextStateName, CardsInHand mCards, Deck drawDeck, Deck discardDeck)
    {
        this.name = name;
        this.nextStateName = nextStateName;
        this.mCards = mCards;
        this.drawDeck = drawDeck;
        this.discardDeck = discardDeck;
        
    }
    public override void Execute(SourceDeck source)
    {
        if (source == SourceDeck.DrawDeck)
        {
            
            mCards.Add(drawDeck.Pop());
        }
        else if (source == SourceDeck.DiscardDeck)
        {
            mCards.Add(discardDeck.Pop());
        }
        else
        {
            Debug.Log("Error, not from any deck");
        }
        
        Debug.Log("Drew finish, currently " + mCards.Count + " cards in hand");

        
        
    }

    
    

}*/
