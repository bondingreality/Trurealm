using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float attackCooldown = 3;
    private float attackRange = .1f;
    public override void Exit()
    {

    }

    public override void Update()
    {
        
        if(parent.MyAttackTime >= attackCooldown && !parent.IsAttacking)
        {
            parent.MyAttackTime = 0;
            parent.StartCoroutine(Attack());
        }

        if(parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if(distance >= parent.MyAttackRange+attackRange && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;

        parent.MyAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);

        parent.IsAttacking = false;
    }
}
