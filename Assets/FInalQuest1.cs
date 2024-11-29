using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInalQuest1 : MonoBehaviour
{
    public GameObject daialogue;
    public GameObject DialoguetoFalse;
    public LayerMask whatIsEnemy;
    public float sightRange;
    public bool enemyinSight;

    // Update is called once per frame
    void Update()
    {
        enemyinSight = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);

        if (enemyinSight == false)
        {
            daialogue.SetActive(true);
            DialoguetoFalse.SetActive(false);
            Destroy(this.gameObject, 2f);
        }
        else
        {
            daialogue.SetActive(false);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
