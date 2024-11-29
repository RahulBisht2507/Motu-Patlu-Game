using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeUpdate : MonoBehaviour
{
    public GameObject PLANE;
    public bool bridgeUpdate;
    public GameObject DRjhatkaDialogue;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == ("Player"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                DRjhatkaDialogue.SetActive(true);
                bridgeUpdate = true;
                Destroy(PLANE);
                
            }
        }
    }

    public void Close()
    {
        DRjhatkaDialogue.SetActive(true);
        bridgeUpdate = true;
        Destroy(PLANE);
    }
}
