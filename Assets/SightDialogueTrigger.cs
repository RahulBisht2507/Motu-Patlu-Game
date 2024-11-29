using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightDialogueTrigger : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float TriggerRange;
    public bool enemyinSight;
    void Update()
    {
        enemyinSight = Physics.CheckSphere(transform.position, TriggerRange, whatIsPlayer);

        if(enemyinSight == true)
        {
            TriggerRange = 0f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TriggerRange);
    }
}
