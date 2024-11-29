using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SamosaCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int samosaCounter = 0;

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Samosa"))
        {
            samosaCounter += 1;
            Text.text = samosaCounter.ToString();
            // add Paticle Effectts
            
        }
    }
}
