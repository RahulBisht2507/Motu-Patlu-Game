using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khusbo : MonoBehaviour
{
    public AudioSource khusbo;
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
            khusbo.Play();
        }
    }
}
