using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class IdleState : BaseState
{
    public override void Enter(Enemy parent)
    {
        base.Enter(parent);
        this.parent.Reset();
    }

    public override void Exit()
    {
    }
    
    public override void Update()
    {
        if (parent.MyTarget != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}