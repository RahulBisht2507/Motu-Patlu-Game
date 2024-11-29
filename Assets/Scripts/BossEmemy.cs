using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossEmemy : MonoBehaviour
{
    [SerializeField] Transform player;
    public Animator motu;
    public Animator ghasite;
    public PlayerMovement Player;
/*    public HealthDemo health;*/
    NavMeshAgent nav;
    public LayerMask whatisGround, whatisPlayer;
    public ParticleSystem Burn;
    public Rigidbody Motu;
    public bool fall = false;
    /*    public ParticleSystem blood;
        public ParticleSystem defeat;*/
    public CombatDemo com;
    /*    public GameObject coin;*/
    /*  public GameObject coinpos;*/

    [Header("Boss Attack CoolDown")]
    public float cooldown;
    float lasttime;
    bool magic;

   [Header("Boss Health")]
    private float BossHealth = 60;
    public Slider healthbar;
    public Slider EaseHealthBar;
    private float lerpspeed = 0.001f;
    public GameObject won;

    [Header("Player Health")]
    public HealthDemo PLayerHealth;

    [Header("Particle Effects")]
    public ParticleSystem RightGlow;
    public ParticleSystem RightTrail;
    public ParticleSystem LeftGlow;
    public ParticleSystem LeftTrail;

    [Header("Attack Audio")]
    public AudioSource Explosion;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked,alreadyCLosedAttack;
    bool patrol;

    //States
    public float sightRange, attackRange,closeRange;
    public bool playerInSightRange, playerInAttackRange,playerInCloseRange;

    private void Start()
    {
        healthbar.value = BossHealth;
    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {

        if (Player.grounded == false)
        {



            motu.SetBool("isFalling", true);
            motu.SetBool("Fall", false);
        }
        // Health Bar System 
        if (healthbar.value != EaseHealthBar.value)
        {
            EaseHealthBar.value = Mathf.Lerp(EaseHealthBar.value, BossHealth, lerpspeed);
        }

        // skelton hit Animation
        if (com.itHit)
        {
            ghasite.SetBool("Hit", true);
           ghasite.ResetTrigger("Chase");
        }
        else
        {
            /*ghasite.SetTrigger("Chase");*/
            ghasite.SetBool("Hit", false);
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);
        playerInCloseRange = Physics.CheckSphere(transform.position, closeRange, whatisPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {  
                Patroling();
        }
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
      /*  if (playerInSightRange && playerInCloseRange) CloseAttack();*/
    }


    /*private void CloseAttack()
    {
        if (!alreadyCLosedAttack)
        {
            ghasite.SetTrigger("attack");
            ghasite.ResetTrigger("Chase");
            ghasite.ResetTrigger("magic");
        }
    }*/
    private void Idling()
    {
        /*ghasite.SetBool("idle", true);
        ghasite.ResetTrigger("Chase");*/

    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
            nav.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 6f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        nav.SetDestination(player.position);
        ghasite.SetTrigger("Chase");
        ghasite.SetBool("Attack", false);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        nav.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (Time.time - lasttime < cooldown)
            {
                if (magic == false)
                {
                    ghasite.SetBool("Attack", true);
                    ghasite.ResetTrigger("Chase");
                    ghasite.SetBool("Magic", false);
                }
            }
            else
            {
                ///Attack code here
                lasttime = Time.time;
                magic = true;
                ghasite.SetBool("Magic", true);
                RightGlow.Play();
                LeftTrail.Play();
                RightGlow.Play();
                RightTrail.Play();
                ghasite.SetBool("Attack", false);
                ghasite.ResetTrigger("Chase");
            }
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else
        {
            RightGlow.Stop();
            LeftTrail.Stop();
            RightGlow.Stop();
            RightTrail.Stop();
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        magic = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }

    public void takeDamage(float damage)
    {
       /* com.itHit = true;*/
        Debug.Log("it hit");
        BossHealth -= damage;
        subhealth(damage);
        ghasite.SetBool("HIt", true);
        ghasite.ResetTrigger("attack");
        if (BossHealth <= 0)
        {
            ghasite.SetTrigger("dead");
            nav.speed = 0;
            ghasite.ResetTrigger("walk");
            ghasite.ResetTrigger("idle");
            Invoke("Explode", 2.7f);
            Invoke("destroy", 3f);
            won.SetActive(true);
            
        }
    }

    public void subhealth(float damage)
    {
        if (BossHealth > 0)
        {
            BossHealth -= damage;
            healthbar.value = BossHealth;
        }
    }

    private void Burned()
    {
        if (Player.grounded && playerInAttackRange)
        {
            PLayerHealth.subhealth(20);
            Explosion.Play();
            Burn.Play();
            Motu.AddExplosionForce(1000f, Motu.transform.position, 2000f);
            /*fall = true;*/
            motu.SetBool("Fall", true);
            motu.SetBool("isFalling", false);
        }
        else if(playerInAttackRange)
        {
            fall = false;
            motu.SetBool("Fall", false);
        }
        else
        {
            fall = false;
        }
    }
}
