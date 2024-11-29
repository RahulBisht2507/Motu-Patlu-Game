using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    [SerializeField]private ParticleSystem death;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Respawn"))
        {
            death.Play();
        }
    }
}
