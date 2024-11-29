using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigantSamosa : MonoBehaviour
{
    public GameObject player;
    float InitialScale = 100;
    float AfterScale = 200;
    float time = .05f;
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
            player.transform.localScale = new Vector3(150,150,150);  
        }
    }
}
