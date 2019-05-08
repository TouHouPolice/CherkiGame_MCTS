using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeldCards:ICards
{

    



    public void Sort()
    {
        Card[] tempArrary = mCards.ToArray();
        for(int i = 7; i >= 0; i--)
        {
            for(int j = 0; j < i; j++)
            {
                if (tempArrary[j].SetType > tempArrary[j + 1].SetType)
                {
                    Card tempCard = tempArrary[j];
                    tempArrary[j] = tempArrary[j + 1];
                    tempArrary[j + 1] = tempCard;
                }
            }
        }
        List<Card> sortedCards = tempArrary.ToList();

        mCards = sortedCards;
    }
    

    public bool CheckVictory()
    {
        HeldCards tempCards=new HeldCards();
        tempCards.mCards = this.mCards;
        
        int completeSetCount = 0;
        for(int i = 0; i < 12; i++)
        {
            if (tempCards.NumOfSetType((SetType)i) == 3)
            {
                completeSetCount += 1;
            }
            else if (tempCards.NumOfSetType((SetType)i) > 3)
            {
                completeSetCount += 1;
                int Removed = 0;

                for (int x = tempCards.mCards.Count - 1; x >= 0; x--)
                {
                    if (Removed < 3)
                    {
                        if (tempCards.mCards[x].SetType == (SetType)i)
                        {
                            tempCards.mCards.RemoveAt(x);
                            Removed++;
                        }
                    }
                    else
                    {
                        i--;
                        break;
                    }
                }

            }
        }
        if (completeSetCount == 3)
        {
            return true;
        }
        return false;
    }
    
    /*public List<Card> GetCardsBySetType(SetType type)
    {
        List<Card> targetCards = new List<Card>();
        foreach(Card card in mCards)
        {
            if (card.SetType == type)
            {
                targetCards.Add(card);
            }
        }
        return targetCards;
    }*/

    public Card GetCardBySetType(SetType setType)
    {
        
        foreach(Card card in mCards)
        {
            
            if (card.SetType == setType)
            {
                return card;
            }
            
        }

        Debug.Log("Card not found, error");
        return null;
    }

}


