using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhasitaDamageDealer : MonoBehaviour
{
    public HealthDemo health;
    public LayerMask layerMask;
    bool dealtdamage;
    bool time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1f, layerMask))
        {
            if (!dealtdamage)
            {
                dealtdamage = true;
                
                health.subhealth(15);
                Debug.Log("ghasiiteee");
            }
        }
        else
        {
            dealtdamage = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1f, Color.green);
        }
    }
}
