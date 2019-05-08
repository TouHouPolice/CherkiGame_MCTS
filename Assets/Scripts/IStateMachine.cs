using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStateMachine
{
    public abstract IState CurrentState { get; }

    

   // public abstract bool Advance();

    public abstract bool IsComplete();
}
