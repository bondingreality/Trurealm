using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected Enemy parent;
    public virtual void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public abstract void Exit();

    public abstract void Update();
}
