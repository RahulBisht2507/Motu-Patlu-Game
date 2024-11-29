using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAppearance : MonoBehaviour
{
    public float sightRange;
    public LayerMask whatisPlayer;
    public bool playerInSightRange,playerhere;
    public PlayerMovement player;
    public Animator portal;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        if (playerInSightRange)
        {
            
            playerhere = true;
            sightRange = 0;
            player.enabled = false;
        }
        if (playerhere)
        {
            
            portal.SetTrigger("Play");
            Invoke("ResetPlayer", 2f);
        }
    }
    public void ResetPlayer()
    {
        
        player.enabled = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
