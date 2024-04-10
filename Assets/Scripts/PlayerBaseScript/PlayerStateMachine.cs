using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
  
  public IPlayer mState { get; set;} 

    public void OnEnterState(IPlayer state) 
    {
        this.mState = state;
        mState.enter();
    }

    public void ChangeState(IPlayer state) 
    {
        mState.exit();
        this.mState = state; 
        mState.enter();
    }
    public void UpdateCurrentState()
    {
        mState.update();
    }
}
