using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2Upgrade : MonoBehaviour
{
    bool ready;
    public bool questUpdate;
    public Quest2Manager QManager;
    private void OnTriggerExit(Collider other)
    {
        if (ready) questUpdate = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                ready = true;
                QManager.quest.SetBool("appear", false);
                QManager.questName.SetBool("Appear", false);
            }

        }
    }

    public void QuestUp()
    {
        ready = true;
        QManager.quest.SetBool("appear", false);
        QManager.questName.SetBool("Appear", false);
    }
}
