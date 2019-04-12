using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an abstract class that all characters needs to inherit from
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected List<CVital> charVitals;
    [SerializeField]
    protected List<CStat> charStats;
    /// <summary>
    /// The Player's movement speed
    /// </summary>
    [SerializeField]
    protected float speed;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    
    public Transform MyTarget { get; set; }

    [SerializeField]
    protected Stat health;

    public Stat MyHealth { get { return health; } }

    /// <summary>
    /// A reference to the character's animator
    /// </summary>
    public Animator MyAnimator { get; set; }

    /// <summary>
    /// The Player's direction
    /// </summary>
    private Vector2 direction = Vector2.zero;
    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    /// <summary>
    /// The Character's rigidbody
    /// </summary>
    private Rigidbody2D myRigidbody;

    /// <summary>
    /// indicates if the character is attacking or not
    /// </summary>
    public bool IsAttacking { get; set; }
    /// <summary>
    /// indicates if the character is attacking or not
    /// </summary>
    public bool IsAlive
    {
        get
        {
            return health.MyCurrentValue > 0;
        }
    }

    /// <summary>
    /// A reference to the attack coroutine
    /// </summary>
    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;
    
    /// <summary>
    /// Indicates if character is moving or not
    /// </summary>
    public bool IsMoving
    {
        get
        {
            return Direction.x != 0 || Direction.y != 0;
        }
    }



    /// <summary>
    /// The player's initialHealth
    /// </summary>
    [SerializeField]
    private float initHealth;
    protected virtual void Start()
    {
        health.Initialize(initHealth, initHealth);

        //Makes a reference to the rigidbody2D
        myRigidbody = GetComponent<Rigidbody2D>();

        //Makes a reference to the character's animator
        MyAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is marked as virtual, so that we can override it in the subclasses
    /// </summary>
    protected virtual void Update ()
    {
        HandleLayers();
	}

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    public void Move()
    {
        if (IsAlive)
        {
            //Makes sure that the player moves
            myRigidbody.velocity = Direction.normalized * Speed;
        }
    }

    /// <summary>
    /// Makes sure that the right animation layer is playing
    /// </summary>
    public void HandleLayers()
    {
        if (IsAlive)
        {
            //Checks if we are moving or standing still, if we are moving then we need to play the move animation
            if (IsMoving)
            {
                ActivateLayer("WalkLayer");

                //Sets the animation parameter so that he faces the correct direction
                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);
            }
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                //Makes sure that we will go back to idle when we aren't pressing any keys.
                ActivateLayer("IdleLayer");
            }
        }
        else
        {
            ActivateLayer("DeathLayer");
        }
    }

    /// <summary>
    /// Activates an animation layer based on a string
    /// </summary>
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName),1);
    }


    public virtual void TakeDamage(float damage, Transform source)
    {
        if(MyTarget == null)
        {
            MyTarget = source;
        }
        
        Debug.Log(damage.ToString());
        health.MyCurrentValue -= damage;

        if(health.MyCurrentValue <= 0)
        {
            Debug.Log("Enemy Died");
            Direction = Vector2.zero;
            myRigidbody.velocity = direction;
            MyAnimator.SetTrigger("Dead");
        }
    }
}
