using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    
    public ParticleSystem coin;
    /* public AudioSource samosa;
     public AudioSource khusbo;*/
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1, 0,0 );
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            /* khusbo.Stop();
             samosa.Play();*/
            coin.transform.SetParent(null);
            coin.Play();
            /*Invoke("Destroy", .5f);*/
            Destroy(gameObject);

            
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
