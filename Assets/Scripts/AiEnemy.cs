using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AiEnemy : MonoBehaviour
{
    [SerializeField] Transform player;
    public Animator motu;
    public Animator skelton;
    public HealthDemo health;
    NavMeshAgent nav;
    public LayerMask whatisGround, whatisPlayer;
    public ParticleSystem blood;
    public ParticleSystem defeat;
    public CombatDemo com;
    public GameObject coin;
    public GameObject coinpos;

    // Skelton Health
    public float skelhealth;
    public Slider healthbar;
    public Slider EaseHealthBar;
    private float lerpspeed = 0.05f;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        // Health Bar System 
        healthbar.value = skelhealth;
        if(healthbar.value != EaseHealthBar.value)
        {
            EaseHealthBar.value = Mathf.Lerp(EaseHealthBar.value, skelhealth, lerpspeed);
        }

        // skelton hit Animation
        if (com.itHit)
        {
            skelton.SetTrigger("hit");
            skelton.ResetTrigger("Chase");
        }
        else
        {
            skelton.SetTrigger("Chase");
            skelton.ResetTrigger("hit");
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        /*if (playerInAttackRange && playerInSightRange) AttackPlayer();*/
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
        skelton.SetTrigger("Chase");
    }

  /*  private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        nav.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            skelton.SetTrigger("attack");
            skelton.ResetTrigger("chase");

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }*/
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
           /* Debug.Log("gotten Hit");*/
            motu.SetBool("isHit", true);
            Invoke("backtoIdle",.2f);
            health.subhealth(5);
        }
    }
    private void backtoIdle()
    {
        motu.SetBool("isHit", false);
    }

    public void takeDamage(float damage)
    {
        com.itHit = true;
        Debug.Log("it hit");
        skelhealth -= damage;
        subhealth(damage);
        if(skelhealth <= 0)
        {
            skelton.SetTrigger("dead");
            nav.speed = 0;
            skelton.ResetTrigger("Chase");
            skelton.ResetTrigger("hit");
            Invoke("Explode",2.7f);
            Invoke("destroy", 3f);
            defeat.transform.SetParent(null);
            coin.transform.position = coinpos.transform.position;
        }
    }
    private void destroy()
    {
        Instantiate(coin);
        Destroy(gameObject);
    }
    private void Explode()
    {
        defeat.Play();
    }
    public void subhealth(float damage)
    {
        if (skelhealth > 0)
        {
            skelhealth -= damage;
            healthbar.value = skelhealth;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
