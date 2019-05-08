using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class  ICards
{
    protected List<Card> mCards;

    
    public  virtual void Remove(Card card)
    {
        mCards.Remove(card);
    }

    public virtual void Add(Card card)
    {
        mCards.Add(card);
        
    }
    public virtual int NumOfSetType(SetType setType)
    {
        int count = 0;
        foreach (Card card in mCards)
        {
            if (card.SetType == setType)
            {
                count++;
            }
        }
        return count;
    }
    
    
}
