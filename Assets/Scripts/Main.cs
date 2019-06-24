using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject[] displayCards;   //The game objects in the game scene that represents the player's cards
    public GameObject[] displayCards_AI; //The game objects in the game scene that represents the computer's cards
    public GameObject topDiscard;     //The game object in the game scene that shows the last discard
    
    public Text currentStateText;         //Text to show whose turn is this
    public Text currentActionText;       //Text to show whether to draw or discard
    public CherkiStateMachine mMachine;  

    public Text drawDeckRemaining;        //The number of cards remained in draw deck

    public EventSystem mEventSystem;   //Reference to event system, used for select detection

    public GameObject endCanvas;        //The game finish canvas
    public Text endText;                  //Text of game finish

    public GameObject[] DiscardCards;       //In Discard Canvas, these objects display what cards have been discarded
    public GameObject topDiscard_Inside;    //In Discard Canvas, show the last discard


    [HideInInspector]
    public  Deck drawDeck;        
    [HideInInspector]
    public  Deck discardDeck;
    [HideInInspector]
    public  CardsInHand playerCardsInHand;
    [HideInInspector]
    public  CardsInHand computerCardsInHand;



    [HideInInspector]
    public static bool isInitialised=false;
    [HideInInspector]
    public static bool isComplete = false;  //If the game has completed

    public MCTSAI mAI;     //Reference to the AI script

    CherkiMachineState.SourceDeck deckToDraw;  //the deck the player is currently selecting

    Card cardToDiscard;                     //refers to the card the player is currently selecting(the card to be discarded)


    [HideInInspector]
    public  Card lastDiscardCard;          //record the last discarded card

    [HideInInspector]
    public  CherkiMachineState.SourceDeck lastDrawDeck;   //record the last deck drawn by a player

    public static int turnCounter = 0;      //Counting the number of turn passed
    public Text turnText;               //Text to show turn count
    
    [HideInInspector]
    public GameObject currentSelect;     //Store the game object currently selected by the player
    public enum Turn
    {
        Human,
        AI,
        Count
    }

    private static Main _instance;

    public static Main Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        turnCounter = 1;
        currentSelect = null;
        drawDeck = new Deck();
        drawDeck.Initialise();  //Generate all the cards

        discardDeck = new Deck();
        playerCardsInHand = new CardsInHand();
        computerCardsInHand = new CardsInHand();

        mMachine = new CherkiStateMachine();
        ShuffleDrawDeck();  //Shuffle the draw deck
        InitialDistribution();  //Distribute 8 cards to each player
        

        playerCardsInHand.Sort();     //Rearrange the hand cards
        computerCardsInHand.Sort();
        UpdateCardsInHand(playerCardsInHand,displayCards);        //Display cards of both players
        UpdateCardsInHand(computerCardsInHand,displayCards_AI);
        deckToDraw = CherkiMachineState.SourceDeck.None;
        cardToDiscard = null;
        UpdateCurrentState();       //These are updates for UI elements
        UpdateDrawDeckRemaining();

       

       
        mAI.enabled = true;       //Enable the AI script
        
        isInitialised = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isInitialised && !isComplete) 
        {
            DetectSelection();
            

           
        }
        
       


    }

    public void InitialDistribution()        //give eight cards to each player
    {
        Debug.Log("Start initial card distribution.");
        for (int i = 0; i < 4; i++)
        {
            playerCardsInHand.Add(drawDeck.Pop());
            playerCardsInHand.Add(drawDeck.Pop());

            computerCardsInHand.Add(drawDeck.Pop());
            computerCardsInHand.Add(drawDeck.Pop());

        }
    }

    public void ShuffleDrawDeck()  //Randomize the draw deck
    {
        drawDeck.Shuffle();
    }


    public void UpdateCardsInHand(CardsInHand handCards,GameObject[] cardUIs)  //Update UI for displaying hand cards
    {
       
        List<Card> mCards = new List<Card>();
        mCards = handCards.GetCards();
        if (mCards.Count == 9)
        {
            cardUIs[8].gameObject.SetActive(true);
        }
        else
        {
            cardUIs[8].gameObject.SetActive(false);
        }
        for (int i = 0; i < mCards.Count(); i++)
        {

                Text valueText = cardUIs[i].transform.GetChild(0).GetComponent<Text>();
                Text suitText = cardUIs[i].transform.GetChild(1).GetComponent<Text>();
                
                suitText.text = mCards[i].Suit.ToString();
                valueText.text = mCards[i].Value.ToString();
            
        }
        

    }


    public void UpdateLastDiscard()  //Update the UI for displaying last discard card
    {
        if (discardDeck.GetTop()!=null)
        {
            topDiscard.SetActive(true);
            Text valueText = topDiscard.transform.GetChild(0).GetComponent<Text>();
            Text suitText = topDiscard.transform.GetChild(1).GetComponent<Text>();

            valueText.text = discardDeck.GetTop().Value.ToString();
            suitText.text = discardDeck.GetTop().Suit.ToString();
        }
        else if (discardDeck.GetTop() == null)
        {
            topDiscard.SetActive(false);
        }

    }

    public void UpdateDrawDeckRemaining() //UI element update
    {
        drawDeckRemaining.text = drawDeck.Count.ToString();
    }

    public void UpdateCurrentState()   //UI element update
    {
        currentStateText.text = mMachine.CurrentState.GetName;

        if (mMachine.CurrentState.hasDrawn)
        {
            currentActionText.text = "Discard";
        }
        else
        {
            currentActionText.text = "Draw";
        }
    }



    public void ResetExecuteTarget()  //Reset the deck to draw and card to discard
    {
        deckToDraw = CherkiMachineState.SourceDeck.None;
        cardToDiscard = null;
    }

    public void HumanExecute()  //This function is used to draw or discard when the button clicked
    {
        if (!CardsInHand.CheckVictory(playerCardsInHand)&&mMachine.CurrentState.GetName=="Player Turn State")
        {
            if (!mMachine.CurrentState.hasDrawn)  //if has not drawn, then it will execute draw action
            {
                
                mMachine.Execute(deckToDraw);
                lastDrawDeck = deckToDraw;
                mAI.MatchAndIterate();  //The AI will do its calculation even it's in player's turn, keeping track of the current state is necessary or the AI will be very dumb
                if (CardsInHand.CheckVictory(playerCardsInHand))
                {
                    UpdateEverything();
                    endCanvas.SetActive(true);
                    endText.text = "Player Win!";
                    isComplete = true;
                    
                }
               
            }
            else            //if has drawn, then it will execute discard action
            {
                
                if (mMachine.Execute(cardToDiscard))

                {
                    lastDiscardCard = cardToDiscard;
                    
                    turnCounter += 1;
                    mAI.MatchAndIterate();  //Same as above
                }
                
            }
       
            UpdateEverything();
        }
    }




    /*public void AIExecute()           //This discarded function was used for the old dumb AI, the script of that AI is in abandoned folder
    {
        if (!mMachine.CheckVictory() && mMachine.CurrentState.GetName=="Computer Turn State")
        {
            if (!mMachine.CurrentState.hasDrawn)
            {
                
                Debug.Log("AI drawing");
                mMachine.Execute(mAI.DrawCard());

                if (mMachine.CheckVictory())
                {
                    UpdateEverything();
                    endCanvas.SetActive(true);
                    endText.text = "Computer Win!";
                    isComplete = true;

                }

            }

            else
            {
                
                Debug.Log("AI Discarding");
                mMachine.Execute(mAI.AbandonCard());
                computerCardsInHand.LeftShiftElement();
                currentTurn = Turn.Human;
            }
            UpdateEverything();
        }
    }*/

    public void AIDraw(CherkiMachineState.SourceDeck sourceDeck) //The funcion is called inside MCTSAI script
    {
        Debug.Log("AI drawing");
        mMachine.Execute(sourceDeck);       //Use FSM to do the action

        if (mMachine.CheckVictory())         //If victory goal met
        {
            UpdateEverything();             
            endCanvas.SetActive(true);    //show end game canvas
            endText.text = "Computer Win!";
            isComplete = true;  //mark the game as completed

        }
        UpdateEverything();
    }

    public void AIDiscard(Card discard)   //same as above
    {

        Debug.Log("AI Discarding");
        mMachine.Execute(discard);
        UpdateEverything();
        
    }

    public void DetectSelection()   //Allow the player to select interactable game objects in the scene
    {                                //When a new object selected, it will be highlighted by giving it different color
                                    //If there is any previously selected object, the previous object will be reset to normal color
        if (mEventSystem.currentSelectedGameObject != null)
        {
            if (mEventSystem.currentSelectedGameObject.layer != 5)
            {
                if (currentSelect != null)
                {
                    currentSelect.GetComponent<Image>().color = new Color(1, 1, 1);
                }
                
                currentSelect = mEventSystem.currentSelectedGameObject;
                currentSelect.GetComponent<Image>().color = new Color(1, 1, 0);
            }
            if (mEventSystem.currentSelectedGameObject.CompareTag("HeldCard"))
            {
                deckToDraw = CherkiMachineState.SourceDeck.None;
                for (int i = 0; i < displayCards.Count(); i++)
                {
                    if (displayCards[i] == mEventSystem.currentSelectedGameObject)
                    {
                        cardToDiscard = playerCardsInHand.GetCards()[i];
                    }
                }
            }
            if (mEventSystem.currentSelectedGameObject.CompareTag("LastDiscard"))
            {
                cardToDiscard = null;
                deckToDraw = CherkiMachineState.SourceDeck.DiscardDeck;
            }
            if (mEventSystem.currentSelectedGameObject.CompareTag("DrawDeck"))
            {
                cardToDiscard = null;
                deckToDraw = CherkiMachineState.SourceDeck.DrawDeck;
            }
        }
    }

    void UpdateDiscards()  //Update the UI element in Discard Canvas
    {
        if (discardDeck.GetTop() != null)
        {
            topDiscard_Inside.SetActive(true);
            Text valueText = topDiscard_Inside.transform.GetChild(0).GetComponent<Text>();
            Text suitText = topDiscard_Inside.transform.GetChild(1).GetComponent<Text>();

            valueText.text = discardDeck.GetTop().Value.ToString();
            suitText.text = discardDeck.GetTop().Suit.ToString();
        }
        else
        {
            if (topDiscard_Inside.active)
            {
                topDiscard_Inside.SetActive(false);
            }
        }

    
        int gameObjectIndex = 0;
        for (int i = 0; i < (int)MeldType.Count; i++)
        {
            int numOfThisMeldType = discardDeck.NumOfMeldCards((MeldType)i);
            if (numOfThisMeldType > 0)
            {
                DiscardCards[gameObjectIndex].SetActive(true);
                DiscardCards[gameObjectIndex].transform.GetChild(0).GetComponent<Text>().text = ((MeldType)i).ToString();
                DiscardCards[gameObjectIndex].transform.GetChild(1).GetComponent<Text>().text = "x" + numOfThisMeldType;
                gameObjectIndex += 1;
            }
  
        }
        
        for (int j = gameObjectIndex; j<DiscardCards.Count(); j++)
        {
            DiscardCards[j].SetActive(false);
        }
    }

    public void UpdateEverything()  //A collection of functions that are likely to be called together, mainly for UI update
    {
        ResetExecuteTarget();
        UpdateCurrentState();
        UpdateDrawDeckRemaining();

        UpdateCardsInHand(playerCardsInHand,displayCards);
        UpdateCardsInHand(computerCardsInHand,displayCards_AI);
        UpdateLastDiscard();
        UpdateDiscards();
        UpdateTurnCounter();
    }


    void UpdateTurnCounter()
    {
        turnText.text = "Turn: " + turnCounter;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Cherki");
    }

}
