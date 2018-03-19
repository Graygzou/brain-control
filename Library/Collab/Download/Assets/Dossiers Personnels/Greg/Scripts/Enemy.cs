using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MovingObject
{

    #region Basic components and scripts retrived with scripting

    // Current components of the gameobject
    private BoxCollider2D m_boxCollider;
    // Steering behavior script that make him move
    private EnemySteeringBehavior steeringBehavior;
    // Reference to all StateMachineBehaviour.
    private StateMachineBehaviour[] states;  

    #endregion

    // Will contains all the player present in the game
    public List<MovingObject> Players { get; set; }
    private List<Transform> neighboursIA;
    [SerializeField] private int nteam = 0;
    public int GetnTeam() { return nteam; }
    public void SetnTeam(int x) { nteam = x; }

    private float curr_temps;

    private GameObject currentDeadBody;

    [SerializeField]
    private RuntimeAnimatorController[] animators;

    #region tweakable variables 

    [Header("Tweakable variables")]
    [Header("Fight")]
    [SerializeField]
    private int damages = 10; // les dommages infligés au joueur en une attaque
    [SerializeField]
    public float tempsEntreChaqueAttaque = 2;
    [SerializeField]
    private float tempsDerniereAttaque = -2;
    [SerializeField]
    private float harmDistance = 0f;

    [Header("Actifs forces")]
    [SerializeField]
    private bool onSeek = false;
    [SerializeField]
    private bool onArrive = false;
    [SerializeField]
    private bool onFollowPath = false;
    [SerializeField]
    private bool onRandom = false;
    [SerializeField]
    private bool onWallAvoidance = false;
    [SerializeField]
    // Flocking
    private bool onFlocking = false;
        [SerializeField]
        private float maxSeparation = 0.5f;

    #endregion


    private void Awake()
    {
        //Get gameobject components
        m_boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        m_ownAnimator = GetComponent<Animator>();

        neighboursIA = new List<Transform>();
    }

    // Use this for initialization
    void Start()
    {
        // Setup root of state machine states
        foreach (State current_state in m_ownAnimator.GetBehaviours<State>())
        {
            // Set the StateMachineBehaviour's reference to an owner to this.
            current_state.m_owner = this;
        }

        // Create usefull scripts
        steeringBehavior = new EnemySteeringBehavior(this, m_grid);

        // Retrieve all the players currently playing
        Players = new List<MovingObject>();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            // Retrieve the script that take care of players position.
            Players.Add(player.GetComponent<MovingObject>());
        }

        // Find his position on the grid
        //InitializePosition();

        m_ownAnimator.SetInteger("team", nteam);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            /*
            if (curr_temps > 0.8f)
            {
                curr_temps = 0f;
                */
                // Update the speed of the animator
                m_ownAnimator.SetFloat("velocity", rb2d.velocity.magnitude);

                // Update the movement of the bot.
                steeringBehavior.FixedUpdate(neighboursIA);
            /*
            }
            curr_temps += Time.time;*/
        }
    }

    #region Parameters accessors

    public bool OnSeek() { return onSeek; }

    public bool OnArrive() { return onArrive; }

    public bool OnFollowPath() { return onFollowPath; }

    public bool OnRandom() { return onRandom; }

    public bool OnWallAvoidance() { return onWallAvoidance; }

    public bool OnFlocking() { return onFlocking; }

    public float GetMaxSeparation() { return maxSeparation; }

    public Rigidbody2D GetRigidbody() { return rb2d; }

    public Collider2D GetCollider() { return m_boxCollider; }

    public float GetHarmDistance() { return harmDistance; }

    public float GetMaxSpeed() { return baseSpeed; }

    public GridNode GetCurrentGridPosition() { return m_currentGridPosition; }

    public RuntimeAnimatorController[] getAnimators()
    {
        return animators;
    }

    #endregion
    
    public void AttackPlayer(MovingObject player)
    {
        // attaque le joueur
        if (Time.time > tempsDerniereAttaque + tempsEntreChaqueAttaque)
        {
            // Set the bool to translate to the good state
            m_ownAnimator.SetBool("isAttacking", true);

            // Deal damages to the player
            player.GetComponent<PlayerStatus>().LoseEnergy(damages);
            tempsDerniereAttaque = Time.time;
        }

    }

    public override void Death()
    {
        base.Death();

        // Create a deadbody sprite at the enemy position.
        currentDeadBody = Instantiate(deadBodySprite, this.transform.position, new Quaternion(0, 0, 0, 0), this.transform);
    }

    public override void Revive(int team)
    {
        base.Revive(team);

        // Create a deadbody sprite at the enemy position.
        GameObject.Destroy(currentDeadBody);

        // Get the right animator
        RuntimeAnimatorController animator = animators[team] as RuntimeAnimatorController;

        // Assign the right animator
        m_ownAnimator.runtimeAnimatorController = animator;
        m_ownAnimator.SetTrigger("spawnAgain");
    }

    /// <summary>
    ///  Useful to detect neighbours and add them to the list
    /// </summary>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            // Add the neighbour to the list.
            neighboursIA.Add(collision.gameObject.transform);
        }
    }

    /// <summary>
    ///  Useful to remove neighbours to the list
    /// </summary>
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            // Remove the neighbour to the list.
            neighboursIA.Remove(collision.gameObject.transform);
        }
    }

}
