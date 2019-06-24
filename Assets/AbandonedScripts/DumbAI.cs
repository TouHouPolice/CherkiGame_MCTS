using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAI 
{
    CardsInHand heldCards;
    Deck drawDeck;
    Deck discardDeck;

    public DumbAI(CardsInHand mCards,Deck mDrawDeck, Deck mDiscardDeck)
    {
        heldCards = mCards;
        drawDeck = mDrawDeck;
        discardDeck = mDiscardDeck;
    }


    public CherkiMachineState.SourceDeck DrawCard()
    {

        if (discardDeck.Count > 0)
        {
            Card pendingCard = discardDeck.GetTop();
            MeldType cardType = pendingCard.MeldType;
            int numOfSameCardsInDiscard = discardDeck.NumOfMeldCards(cardType);
            int numOfSameCardsInHand = heldCards.NumOfMeldCards(cardType);
            int numOfCardsRevealed = numOfSameCardsInHand + numOfSameCardsInDiscard;


            if (cardType != MeldType.RedFlower && cardType != MeldType.WhiteFlower && cardType != MeldType.OldThousand)
            {
                int numOfCardsUnRevealed = 12 - numOfCardsRevealed;
                if (numOfCardsUnRevealed - (numOfSameCardsInHand % 3) >= 0 && numOfSameCardsInHand % 3 > 0)
                {
                    return CherkiMachineState.SourceDeck.DiscardDeck;
                }
                else
                {
                    //return drawDeck.Pop();
                    return CherkiMachineState.SourceDeck.DrawDeck;
                }


            }
            else
            {
                int numOfCardsUnRevealed = 4 - numOfCardsRevealed;
                if (numOfCardsUnRevealed - (numOfSameCardsInHand % 3) >= 0 && numOfSameCardsInHand % 3 > 0)
                {
                    // return discardDeck.Pop();
                    return CherkiMachineState.SourceDeck.DiscardDeck;
                }
                else
                {
                    //return drawDeck.Pop();
                    return CherkiMachineState.SourceDeck.DrawDeck;
                }
            }
        }
        else
        { //return drawDeck.Pop(); 
            return CherkiMachineState.SourceDeck.DrawDeck;


        }
    }

    public Card AbandonCard()
    {


        for(int i = 11; i >= 0; i--)
        {
            
            
            int numOfSameCardsInHand = heldCards.NumOfMeldCards((MeldType)i);

            

            if (numOfSameCardsInHand > 0)
            {
                int numOfSameCardsInDiscard = discardDeck.NumOfMeldCards((MeldType)i);
                int numOfCardsRevealed = numOfSameCardsInHand + numOfSameCardsInDiscard;

                if (i >2 )
                {
                    int numOfCardsUnRevealed = 12 - numOfCardsRevealed;
                    if (numOfCardsUnRevealed + (numOfSameCardsInHand % 3) < 3 )
                    {
                        Card cardToDiscard = heldCards.GetCardByMeldType((MeldType)i);
                        return cardToDiscard;
                    }
                }
                else
                {
                    int numOfCardsUnRevealed = 4 - numOfCardsRevealed;
                    if(numOfCardsUnRevealed + (numOfSameCardsInHand % 3) < 3)
                    {
                        Card cardToDiscard = heldCards.GetCardByMeldType((MeldType)i);
                        return cardToDiscard;
                    }
                }
            }
        }
        

        for(int i = 0; i <= 11; i++)
        {
            int numOfSameCardsInHand = heldCards.NumOfMeldCards((MeldType)i);
            if (numOfSameCardsInHand > 0)
            {
                Card cardToDiscard = heldCards.GetCardByMeldType((MeldType)i);
                return cardToDiscard;
            }

            
                
            
        }
        Debug.Log("Error, no card to be discarded, return null");
        return null;
    }
}
