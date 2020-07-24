using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MCTSAI : MonoBehaviour        //Orignial code= https://github.com/aldidoanta/TicTacToeMCTS
{
    public static Main.Turn myTurn = Main.Turn.AI;  //Refers to what turn the AI belongs to
    public int iterationNumber;                   //The number decides how many times the AI will iterate
    [HideInInspector] public TreeNode treeNode;


    TreeNode tempTreeNode;

    bool flag = false;

    void Start()
    {


        initAI();        //This line of code to initiate the TreeNode is practically useless. Later it always generate a new node when because there is no children

        
    }


    void Update()
    {
        
        if (Main.isInitialised)
        {
            
            if (Main.Instance.mMachine.CurrentState.MyTurn == myTurn && !Main.isComplete)
            {
                
                MCTSIterate();         //Get state that matches the current game state, then expand and simulate
                treeNode = treeNode.select();  //Based on calculation, select the best node to proceed
                
                if (!Main.Instance.mMachine.CurrentState.hasDrawn)   //if has not drawn
                {

                    
                    Main.Instance.AIDraw(treeNode.state.lastDrawDeck);  //Draw from a deck according to the node we just selected
                    
                    
                    Main.Instance.lastDrawDeck = (treeNode.state.lastDrawDeck);  //Update game/board information

                    if (CardsInHand.CheckVictory(Main.Instance.computerCardsInHand))
                    {
                        treeNode.state.stateResult = MCTSState.Result.AIWin;
                    }
                    

                }

                else if(Main.Instance.mMachine.CurrentState.hasDrawn)  //If has drawn
                {
                   
                    Main.turnCounter += 1;
                    Main.Instance.AIDiscard(treeNode.state.lastDiscard);  //Draw a card according to the node selected
                    
                   
                    Main.Instance.lastDiscardCard = treeNode.state.lastDiscard; //Update game/board information


                    treeNode.expand();  //Now the children number has became zero, we need to iterate to get children
                    
                }

                

                
            }
        }
    }

    public void initAI()   //Pratically useless function, initiating the treeNode
    {
        Debug.Log("Init AI");
        treeNode=new TreeNode(new MCTSState(Main.Instance.mMachine.CurrentState.MyTurn, Main.Instance.drawDeck, Main.Instance.discardDeck, Main.Instance.playerCardsInHand, Main.Instance.computerCardsInHand, Main.Instance.mMachine.CurrentState.hasDrawn, null,CherkiMachineState.SourceDeck.None));
        
        
    }

    public void MatchAndIterate()
    {

        FindMatchedNode();

        treeNode.expand();
        
    }

    public void MCTSIterate()
    {
        
        for (int i = 0; i < iterationNumber; i++)
        {
            //Debug.Log("Iteration: "+i);
            treeNode.iterateMCTS();
        }
        

    }

    public void FindMatchedNode()   //Among all the children nodes, find the one whose state is exactly the same as the game's current state
    {                               //("state" is not FSM state, it is the MCTSState that contain all the information of the game at a certain point)
        flag = false;               //Replace current treeNode with the matched childe node
        if (treeNode.children.Count > 0)   //If the current treeNode does not have any children(Happen the first time it runs), create a new node
        {
            //Debug.Log("treeNode children >0");
            if (Main.Instance.lastDiscardCard != null)
            {
                foreach (TreeNode child in treeNode.children)   //Loop through all the children
                {
                    if (
                        child.state.lastDiscard == Main.Instance.lastDiscardCard       //find the child that match the current state of the game

                        && child.state.lastDrawDeck == Main.Instance.lastDrawDeck
                            
                            )
                    {
                       // Debug.Log("Found the target node");
                        treeNode = child;
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag) Debug.Log("unreachable code");
        }
        else
        {
            Debug.Log("new node created");
            treeNode = new TreeNode(new MCTSState(Main.Instance.mMachine.CurrentState.MyTurn, Main.Instance.drawDeck, Main.Instance.discardDeck, Main.Instance.playerCardsInHand, Main.Instance.computerCardsInHand, Main.Instance.mMachine.CurrentState.hasDrawn, Main.Instance.lastDiscardCard, Main.Instance.lastDrawDeck));
        }

    }
}
