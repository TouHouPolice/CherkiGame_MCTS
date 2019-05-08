using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    int value=0;
    Suit suit;
    SetType setType;

    public Card(Suit mSuit, int mValue,SetType mSetType)
    {
        this.value = mValue;
        this.suit = mSuit;
        this.setType = mSetType;
    }

    public int Value{get{ return value; }}
    public Suit Suit { get { return suit; } }
    public SetType SetType { get { return setType; } }
}

public enum SetType
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    RedFlower,
    WhiteFlower,
    OldThousand,
}

public enum Suit
{
    Coin,
    String,
    Myriad,
    RedFlower,
    WhiteFlower,
    OldThousand,
    Empty
}