using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUi : MonoBehaviour
{
    public GameObject Ui;
/*    public Animator boss;*/
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            Ui.SetActive(true);
        }
    }
}
