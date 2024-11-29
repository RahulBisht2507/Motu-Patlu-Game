using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushHeadAttack : MonoBehaviour
{
    public Rigidbody motu;
    public float force;
    public Animator Motu;
    public HealthDemo health;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            motu.AddForce(force, 0, 0);
            Motu.SetBool("isHit",true);
            Invoke("Stop", .5f);
            health.subhealth(10);
        }
    }
    private void Stop()
    {
        Motu.SetBool("isHit", false);
    }
}
