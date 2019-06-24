using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStateMachine           //Abandonded, uncessary
{
    public abstract CherkiMachineState CurrentState { get; }

    



    public abstract bool CheckVictory();
}
