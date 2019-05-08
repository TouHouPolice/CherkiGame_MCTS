using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    protected string name;
    protected string nextStateName;

    

    public virtual void Execute(Card card,SourceDeck source) {  }
    public virtual void Enter()
    {
        Debug.Log("Entering" + name);
    }

    

    public virtual string GetName { get { return name; } }
    public virtual string GetNextStateName { get { return nextStateName; } }


    public enum SourceDeck
    {
        DrawDeck,
        DiscardDeck,
        None
    }
}
