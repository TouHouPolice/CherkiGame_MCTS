using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerState : IState
{
    public DealerState(string name, string nextStateName)
    {
        this.name = name;
        this.nextStateName = nextStateName;
    }

    public  void DistributeCards(ref CardsInHand playerCardsInHand, ref CardsInHand componentCardsInHand, ref Deck drawDeck, ref Deck discardDeck)
    {
        for(int i = 0; i < 4; i++)
        {
            playerCardsInHand.Add(drawDeck.Pop());
            playerCardsInHand.Add(drawDeck.Pop());

            componentCardsInHand.Add(drawDeck.Pop());
            componentCardsInHand.Add(drawDeck.Pop());

        }
    }
}
