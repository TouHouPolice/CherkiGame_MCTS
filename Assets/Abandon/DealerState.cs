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

    public  void DistributeCards(ref HeldCards playerHeldCards, ref HeldCards componentHeldCards, ref Deck drawDeck, ref Deck discardDeck)
    {
        for(int i = 0; i < 4; i++)
        {
            playerHeldCards.Add(drawDeck.Pop());
            playerHeldCards.Add(drawDeck.Pop());

            componentHeldCards.Add(drawDeck.Pop());
            componentHeldCards.Add(drawDeck.Pop());

        }
    }
}
