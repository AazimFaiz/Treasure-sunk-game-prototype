using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState
{
    public string name;
    protected PlayerStateController stateMachine;

    public PlayerBaseState(string name, PlayerStateController stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void Update() { }
    public virtual void OnExit() { }
}