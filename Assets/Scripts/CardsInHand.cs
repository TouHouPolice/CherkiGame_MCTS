using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardsInHand : ICards       //The class represents the cards a player is holding
{

    public CardsInHand()
    {
        mCards = new List<Card>();
    }

    public CardsInHand(List<Card> cloneList)
    {
        mCards = cloneList;
    }



    public void Sort()            //Sort the cards based on their meld type
    {                             //substantially based on their value and suit
        Card[] tempArrary = mCards.ToArray();
        for(int i = 7; i >= 0; i--)
        {
            for(int j = 0; j < i; j++)
            {
                if (tempArrary[j].MeldType > tempArrary[j + 1].MeldType)
                {
                    if (tempArrary[j] != null)
                    {
                        Card tempCard = tempArrary[j];
                        tempArrary[j] = tempArrary[j + 1];
                        tempArrary[j + 1] = tempCard;
                    }
                }
            }
        }
        List<Card> sortedCards = tempArrary.ToList();

        mCards = sortedCards;
    }
    

    public static bool CheckVictory(CardsInHand mCardsInHand)   //Check if there are 3 completed meld
    {
        
        
        int completeSetCount = 0;
        for(int i = 0; i < 12; i++)
        {
            int numOfCurrentMeldType = 0;
            numOfCurrentMeldType = mCardsInHand.NumOfMeldCards((MeldType)i);
            if (numOfCurrentMeldType == 3)
            {
                completeSetCount += 1;
            }
            else if (numOfCurrentMeldType > 3)
            {
                completeSetCount += (numOfCurrentMeldType - (numOfCurrentMeldType % 3)) / 3;
            
            }
        }
        if (completeSetCount == 3)
        {
            return true;
        }
        return false;
    }
    


    public Card GetCardByMeldType(MeldType meldType)   //Get the first card found of the meld type
    {
        
        foreach(Card card in mCards)
        {
            
            if (card.MeldType == meldType)
            {
                return card;
            }
            
        }

        //Debug.Log("Card not found, error");
        return null;
    }

    

}


