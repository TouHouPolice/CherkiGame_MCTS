using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Deck : ICards
{
    public  void Initialise()
    {
        
        List<Card> allCards = new List<Card>();

        

        for (int x = 0; x < 6; x++)
        {
            if (x < 3)
            {
                for (int y = 0; y < 9; y++)
                {
                    allCards.Add(new Card((Suit)x, y,(SetType)y));
                    allCards.Add(new Card((Suit)x, y, (SetType)y));
                    allCards.Add(new Card((Suit)x, y, (SetType)y));
                    allCards.Add(new Card((Suit)x, y, (SetType)y));
                }
            }
            else
            {
                allCards.Add(new Card((Suit)x, 1,(SetType)(x-3+9)));
                allCards.Add(new Card((Suit)x, 1, (SetType)(x - 3 + 9)));
                allCards.Add(new Card((Suit)x, 1, (SetType)(x - 3 + 9)));
                allCards.Add(new Card((Suit)x, 1, (SetType)(x - 3 + 9)));
            }
        }

        mCards = allCards;//temporary
        /*Card string_1 = new Card(Suit.String, 1);
        Card string_2 = new Card(Suit.String, 2);
        Card string_3 = new Card(Suit.String, 3);
        Card string_4 = new Card(Suit.String, 4);
        Card string_5 = new Card(Suit.String, 5);
        Card string_6 = new Card(Suit.String, 6);
        Card string_7 = new Card(Suit.String, 7);
        Card string_8 = new Card(Suit.String, 8);
        Card string_9 = new Card(Suit.String, 9);

        Card Coin_1 = new Card(Suit.Coin, 1);
        Card Coin_2 = new Card(Suit.Coin, 1);
        Card Coin_3 = new Card(Suit.Coin, 1);
        Card Coin_4 = new Card(Suit.Coin, 1);
        Card Coin_5 = new Card(Suit.Coin, 1);
        Card Coin_6 = new Card(Suit.Coin, 1);
        Card Coin_7 = new Card(Suit.Coin, 1);
        Card Coin_8 = new Card(Suit.Coin, 1);
        Card Coin_9 = new Card(Suit.Coin, 1);*/


        /*Card string_1_1 = new Card(Suit.String, 1);
        Card string_1_2 = new Card(Suit.String, 1);
        Card string_1_3 = new Card(Suit.String, 1);
        Card string_1_4 = new Card(Suit.String, 1);

        Card string_2_1 = new Card(Suit.String, 2);
        Card string_2_2 = new Card(Suit.String, 2);
        Card string_2_3 = new Card(Suit.String, 2);
        Card string_2_4 = new Card(Suit.String, 2);

        Card string_3_1 = new Card(Suit.String, 3);
        Card string_3_2 = new Card(Suit.String, 3);
        Card string_3_3 = new Card(Suit.String, 3);
        Card string_3_4 = new Card(Suit.String, 3);

        Card string_4_1 = new Card(Suit.String, 4);
        Card string_4_2 = new Card(Suit.String, 4);
        Card string_4_3 = new Card(Suit.String, 4);
        Card string_4_4 = new Card(Suit.String, 4);

        Card string_5_1 = new Card(Suit.String, 5);
        Card string_5_2 = new Card(Suit.String, 5);
        Card string_5_3 = new Card(Suit.String, 5);
        Card string_5_4 = new Card(Suit.String, 5);

        Card string_6_1 = new Card(Suit.String, 6);*/


        
    }

    public virtual Card Pop()
    {
        Card temp = mCards.Last();
        mCards.Remove(mCards.Last());
        return temp;
    }
    public Card GetTop()
    {
        return mCards.Last();
    }

    public List<Card> GetCards()
    {
        return mCards;
    }


}
