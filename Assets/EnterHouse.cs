using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnterHouse : MonoBehaviour
{
    public Animator ui;
    public Animator quest;
    public Animator questName;
    public bool done = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            ui.gameObject.SetActive(true);
            ui.SetBool("Here", true);
            Invoke("end", 1.5f);
            done = true;
            quest.SetBool("appear", false);
            questName.SetBool("Appear", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == ("Player"))
        {
            Invoke("Des", 1.7f);
        }
    }
    private void end()
    {
        ui.SetBool("Here", false);
        
    }
    private void Des()
    {
        Destroy(this.gameObject);
    }
}
