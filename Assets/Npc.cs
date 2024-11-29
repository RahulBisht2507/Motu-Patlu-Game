using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public Animator boy;
    NavMeshAgent nav;
    public LayerMask whatisGround, whatisPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //States
    public float sightRange;
    public bool playerInSightRange;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        Walk();
    }

    private void Update()
    {

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);

        if (!playerInSightRange) Patroling();
     /*   if (playerInSightRange) Stop();*/

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
        if (distanceToWalkPoint.magnitude < 2f)
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
  /*  private void Stop()
    {
        boy.SetBool("Stop", true);
        boy.ResetTrigger("Walk");
        boy.ResetTrigger("Run");
        nav.speed = 0f;
        Invoke("Idle", .4f);
    }*/
  /*  private void Idle()
    {
        boy.SetBool("Stop", false);
        boy.ResetTrigger("idle");
        boy.ResetTrigger("Walk");
        boy.ResetTrigger("Run");
    }*/
    private void Walk()
    {
        nav.speed = 5f;
        boy.SetTrigger("Walk");
        boy.ResetTrigger("Run" +
            "");
        Invoke("Run", 5f);
    }
    private void Run()
    {
        nav.speed = 7f;
        boy.SetTrigger("Run");
        boy.ResetTrigger("Walk");
        Invoke("Walk", 5f);
    }

}
