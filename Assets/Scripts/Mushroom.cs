using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Mushroom : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Animator motu;
    NavMeshAgent nav;
    public ParticleSystem hit;
    [SerializeField] private ParticleSystem defeat;
    [SerializeField] private HealthDemo health;
    [SerializeField] private CineShake cine;
    [SerializeField] private LayerMask whatisGround, whatisPlayer;
    public CombatDemo com;
    public AxeDealer axe;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coinpos;
    //Patroling
    [SerializeField] private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;

    // Health System
    private float MushHealth = 3;
    [SerializeField] private Slider healthbar;
    [SerializeField] private Slider EaseHealthBar;
    private float lerpspeed = 0.05f;

    // Animator
    [SerializeField] private Animator mushroom;
    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    void Update()
    {

        // Health Bar System 
        healthbar.value = MushHealth;
        if (healthbar.value != EaseHealthBar.value)
        {
            EaseHealthBar.value = Mathf.Lerp(EaseHealthBar.value, MushHealth, lerpspeed);
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        // Mushroom hit Animation
        if (com.itHit)
        {
            mushroom.SetBool("isHit", true);
        }
        else
        {
            mushroom.SetBool("isHit", false);
            motu.SetTrigger("walk");
        }

        if (axe.ithit)
        {
            mushroom.SetBool("isHit", true);
        }
        else
        {
            mushroom.SetBool("isHit", false);
            motu.SetTrigger("walk");
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            nav.SetDestination(walkPoint);
            mushroom.SetTrigger("walk");
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 4f)
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
        mushroom.SetTrigger("walk");
        mushroom.ResetTrigger("attack");
        mushroom.ResetTrigger("idle");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        nav.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            ///End of attack code
            mushroom.SetTrigger("attack");
            mushroom.ResetTrigger("walk");
            mushroom.ResetTrigger("idle");
            alreadyAttacked = true;
            Invoke("AttackAgain", 1f);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void AttackAgain()
    {
        mushroom.SetTrigger("idle");
        mushroom.ResetTrigger("attack");
        mushroom.ResetTrigger("walk");
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    public void takeDamage(float damage)
    {
        com.itHit = true;
        Debug.Log("it hit");
        MushHealth -= damage;
        subhealth(damage);
        if (MushHealth <= 0)
        {
            mushroom.SetTrigger("dead");
            nav.speed = 0;
            mushroom.ResetTrigger("walk");
            mushroom.ResetTrigger("idle");
            Invoke("Explode", 2.7f);
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
        if (MushHealth > 0)
        {
            MushHealth -= damage;
            healthbar.value = MushHealth;
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
