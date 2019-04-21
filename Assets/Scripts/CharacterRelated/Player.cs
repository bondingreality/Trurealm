using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the player script, it contains functionality that is specific to the Player
/// </summary>
public class Player : Character
{
    private static Player instance;
    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    /// <summary>
    /// The player's mana
    /// </summary>
    [SerializeField]
    private Stat mana;

    /// <summary>
    /// The player's initial mana
    /// </summary>
    private float initMana = 50;
    
    [SerializeField]
    private Transform[] exitPoints;
    
    private Vector3 min, max;

    [SerializeField]
    private Block[] visibilityBlocks;
    
    private int exitIndex = 2;

    [SerializeField]
    private GearSocket[] gearSockets;

    protected override void Start()
    {
        mana.Initialize(initMana, initMana);;
        
        base.Start();
    }

    /// <summary>
    /// We are overriding the characters update function, so that we can execute our own functions
    /// </summary>
    protected override void Update()
    {
        //Executes the GetInput function
        GetInput();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.y), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);

        base.Update();
    }

    /// <summary>
    /// Listen's to the players input
    /// </summary>
    private void GetInput()
    {
        Direction = Vector2.zero;

        ///THIS IS USED FOR DEBUGGING ONLY
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        if (Input.GetKey(KeybindManager.GetKeyCode(KeyBinderKeys.UP)))//Moves up
        {
            Direction += Vector2.up;
            exitIndex = 0;
        }
        if (Input.GetKey(KeybindManager.GetKeyCode(KeyBinderKeys.LEFT))) //Moves left
        {
            Direction += Vector2.left; 
            exitIndex = 3;
        }
        if (Input.GetKey(KeybindManager.GetKeyCode(KeyBinderKeys.DOWN))) //Moves down
        {
            Direction += Vector2.down;
            exitIndex = 2;
        }
        if (Input.GetKey(KeybindManager.GetKeyCode(KeyBinderKeys.RIGHT))) //Moves right
        {
            Direction += Vector2.right;
            exitIndex = 1;
        }
        if(IsMoving)
        {
            StopAttack();
        }

        foreach(string action in KeybindManager.MyInstance.MyActionbinds.Keys)
        {
            if(Input.GetKeyDown(KeybindManager.MyInstance.MyActionbinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private Transform currentTarget;
    public void CastSpell(string spellName)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight(MyTarget)) //Chcks if we are able to attack
        {
            currentTarget = MyTarget;
            attackRoutine = StartCoroutine(Attack(spellName));
        }
    }

    /// <summary>
    /// A co routine for attacking
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack(string spellName)
    {
        Spell mySpell = SpellBook.MyInstance.CastSpell(spellName);

        IsAttacking = true; //Indicates if we are attacking

        MyAnimator.SetBool("attack", IsAttacking); //Starts the attack animation

        foreach(GearSocket g in gearSockets)
        {
            g.MyAnimator.SetBool("attack", IsAttacking);
        }

        yield return new WaitForSeconds(mySpell.MyCastTime); //This is a hardcoded cast time, for debugging

        if (currentTarget != null && InLineOfSight(currentTarget))
        {
            SpellScript s = Instantiate(mySpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(this.transform, currentTarget, mySpell.damage);
        }

        StopAttack(); //Ends the attack
    }

    private bool InLineOfSight(Transform target)
    {
        Vector3 targetDir = (target.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, Vector2.Distance(transform.position, target.transform.position), LayerMask.GetMask("Visibility"));

        if (hit.collider == null)
        {
            return true;
        }

        return false;
    }

    private void Block()
    {
        foreach(var b in visibilityBlocks)
        {
            b.Deactivate();
        }

        visibilityBlocks[exitIndex].Activate();
    }
    
    /// <summary>
    /// Stops the attack
    /// </summary>
    public void StopAttack()
    {
        SpellBook.MyInstance.StopSpell();

        IsAttacking = false; //Makes sure that we are not attacking

        MyAnimator.SetBool("attack", IsAttacking); //Stops the attack animation

        foreach (GearSocket g in gearSockets)
        {
            g.MyAnimator.SetBool("attack", IsAttacking);
        }

        if (attackRoutine != null) //Checks if we have a reference to an co routine
        {
            StopCoroutine(attackRoutine);
        }
    }

    public override void HandleLayers()
    {
        base.HandleLayers();

        if(IsMoving)
        {
            foreach(GearSocket g in gearSockets)
            {
                g.SetXandY(Direction.x, Direction.y);
            }
        }
    }
    public override void ActivateLayer(string layerName)
    {
        base.ActivateLayer(layerName);

        foreach(GearSocket g in gearSockets)
        {
            g.ActivateLayer(layerName);
        }
    }
}
