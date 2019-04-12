using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private float initAgroRange;

    [SerializeField]
    private LootTable lootTable;


    private IState currentState;
    public Vector3 MyStartPosition { get; set; }
    public float MyAttackRange { get; set; }
    public float MyAttackTime { get; set; }
    public float MyAgroRange { get; set; }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAgroRange;
        }
    }

    protected void Awake()
    {
        MyStartPosition = transform.position;
        MyAgroRange = initAgroRange;
        MyAttackRange = 1;
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }

            currentState.Update();
        }
        base.Update();
    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;
        
        return base.Select();
    }

    public override void Deselect()
    {
        healthGroup.alpha = 0;

        base.Deselect();
    }

    public override void TakeDamage(float damage, Transform source)
    {
        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);

            OnHealthChanged(MyHealth.MyCurrentValue);
        }
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetTarget(Transform target)
    {
        if(MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            //MyAgroRange = initAgroRange;
            MyAgroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
        this.MyTarget = null;
        this.MyAgroRange = initAgroRange;
        this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }

    public override void Interact()
    {
        if(!IsAlive)
        {
            lootTable.ShowLoot();
        }
    }
}
