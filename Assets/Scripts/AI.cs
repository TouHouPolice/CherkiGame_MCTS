using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI 
{
    HeldCards heldCards;
    Deck drawDeck;
    Deck discardDeck;

    public AI(HeldCards mCards,Deck mDrawDeck, Deck mDiscardDeck)
    {
        heldCards = mCards;
        drawDeck = mDrawDeck;
        discardDeck = mDiscardDeck;
    }


    public Card SelectCard()
    {
        Card pendingCard = discardDeck.GetTop();
        SetType cardType = pendingCard.SetType;

        int numOfSameCardsInDiscard = discardDeck.NumOfSetType(cardType);
        int numOfSameCardsInHand = heldCards.NumOfSetType(cardType);
        int numOfCardsRevealed = numOfSameCardsInHand + numOfSameCardsInDiscard;
        
        
        if(cardType!=SetType.RedFlower&& cardType != SetType.WhiteFlower && cardType != SetType.OldThousand)
        {
            int numOfCardsUnRevealed = 12 - numOfCardsRevealed;
            if (numOfCardsUnRevealed-(numOfSameCardsInHand % 3)>=0 && numOfSameCardsInHand % 3 > 0)
            {
                return discardDeck.Pop();
            }
            else
            {
                return drawDeck.Pop();
            }

            
        }
        else
        {
            int numOfCardsUnRevealed = 4 - numOfCardsRevealed;
            if (numOfCardsUnRevealed - (numOfSameCardsInHand % 3) >= 0 && numOfSameCardsInHand % 3 > 0)
            {
                return discardDeck.Pop();
            }
            else
            {
                return drawDeck.Pop();
            }
        }
        

        
    }

    public Card AbandonCard()
    {


        for(int i = 11; i >= 0; i--)
        {
            
            
            int numOfSameCardsInHand = heldCards.NumOfSetType((SetType)i);

            

            if (numOfSameCardsInHand > 0)
            {
                int numOfSameCardsInDiscard = discardDeck.NumOfSetType((SetType)i);
                int numOfCardsRevealed = numOfSameCardsInHand + numOfSameCardsInDiscard;

                if (i <= 8)
                {
                    int numOfCardsUnRevealed = 12 - numOfCardsRevealed;
                    if (numOfCardsUnRevealed - (numOfSameCardsInHand % 3) < 0 )
                    {
                        Card cardToDiscard = heldCards.GetCardBySetType((SetType)i);
                        return cardToDiscard;
                    }
                }
                else
                {
                    int numOfCardsUnRevealed = 4 - numOfCardsRevealed;
                    if(numOfCardsUnRevealed - (numOfSameCardsInHand % 3) < 0)
                    {
                        Card cardToDiscard = heldCards.GetCardBySetType((SetType)i);
                        return cardToDiscard;
                    }
                }
            }
        }
        

        for(int i = 11; i >= 0; i--)
        {
            int numOfSameCardsInHand = heldCards.NumOfSetType((SetType)i);
            if (numOfSameCardsInHand > 0)
            {
                Card cardToDiscard = heldCards.GetCardBySetType((SetType)i);
                return cardToDiscard;
            }

            
                
            
        }
        Debug.Log("Error, no card to be discarded, return null");
        return null;
    }
}
