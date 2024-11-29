using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject islandpos;
    public GameObject player;
    public AudioSource tel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            tel.Play();
            player.transform.position = islandpos.transform.position;
           /* player.transform.rotation = islandpos.transform.rotation;*/

        }
    }
}
