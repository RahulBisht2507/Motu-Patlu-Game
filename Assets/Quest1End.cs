using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1End : MonoBehaviour
{
    public bool quest1End;
    bool waht;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {
            if (Input.GetKey(KeyCode.E)) { waht = true; }
            
        }
    }

    public void Talked()
    {
        waht = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (waht)
        {
            if (other.tag == ("Player"))
            {
                quest1End = true;
            }
        }
       
    }
}
