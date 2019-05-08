using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherkiStateMachine : IStateMachine
{
    static Deck drawDeck;
    static Deck discardDeck;
    static HeldCards playerHeldCards;
    static HeldCards computerHeldCards;
    public IState.SourceDeck source;


    List<IState> mStates;
    IState currentState;

    DrawState playerDrawState = new DrawState("Player Draw State", "Player Discard State", playerHeldCards, drawDeck, discardDeck);
    DrawState computerDrawState = new DrawState("Computer Draw State", "Computer Discard State", computerHeldCards, drawDeck, discardDeck);
    DiscardState playerDiscardState = new DiscardState("Player Discard State", "Computer Draw State", playerHeldCards,  discardDeck);
    DiscardState computerDiscardState = new DiscardState("Computer Discard State", "Player Draw State", computerHeldCards, discardDeck);


    /*List<TurnState> mStates;
    TurnState currentState ;

    TurnState playerTurnState = new TurnState("Player Turn", "Computer Turn", playerHeldCards, drawDeck, discardDeck);
    TurnState computerTurnState = new TurnState("Computer Turn", "Player Turn", computerHeldCards, drawDeck, discardDeck);*/

    public CherkiStateMachine()
    {
        drawDeck = new Deck();
        drawDeck.Initialise();
        
        discardDeck = new Deck();
        playerHeldCards = new HeldCards();
        computerHeldCards = new HeldCards();
        mStates.Add(playerDrawState);
        mStates.Add(playerDiscardState);
        mStates.Add(computerDrawState);
        mStates.Add(computerDiscardState);

        /*mStates.Add(playerTurnState);
        mStates.Add(computerTurnState);
        currentState = playerTurnState;*/

    }
   
    
    



    public override IState CurrentState
    {
        get { return currentState; }
    }

     
    public bool Execute(Card card,IState.SourceDeck source)
    {
        currentState.Execute(card, source);

        foreach (IState state in mStates)
        {
            if (currentState.GetNextStateName == state.GetName)
            {
                currentState = state;
                return true;
            }

        }
        Debug.Log("Advance failed, error");
        return false;
    }
    

    /*public override bool Advance()
    {
        
    }*/

    
    public override bool IsComplete()
    {
        return true;
    }

    public void InitialDistribution()
    {
        Debug.Log("Start initial card distribution.");
        for (int i = 0; i < 4; i++)
        {
            playerHeldCards.Add(drawDeck.Pop());
            playerHeldCards.Add(drawDeck.Pop());

            computerHeldCards.Add(drawDeck.Pop());
            computerHeldCards.Add(drawDeck.Pop());

        }
    }
    /*public  bool DiscardCard(Card card)
     {

        currentState.DiscardOneCard(card);
        foreach (TurnState state in mStates)
        {
            if (currentState.GetNextStateName == state.GetName)
            {
                currentState = state;
                return true;
            }
        }
        System.Console.WriteLine("Invalid state.");
        return false;
    }

    public bool PickCardFromDeck()
    {
        currentState.PickCardFromDeck();
        return true;
    }

    public bool PickDiscardedCard()
    {
        currentState.PickDiscardedCard();
        return true;
    }*/
}
