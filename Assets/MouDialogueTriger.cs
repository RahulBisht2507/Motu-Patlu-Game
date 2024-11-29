using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouDialogueTriger : MonoBehaviour
{

    public MotuDialogue Motudialogue;
    public SightDialogueTrigger sight;
    public PlayerMovement player;
    public GameObject next;
    public DialogueManager dialogueManager;

    // Update is called once per frame
    void Update()
    {
        if (sight.enemyinSight == true)
        {
            player.motu.SetBool("isMoving", false);
            player.walk.enabled = false;
            player.enabled = false;
            next.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Invoke("TriggerDialogue", 0f);
        }
        if (dialogueManager.endDialohue)
        {
            dialogueManager.endDialohue = false;
            player.enabled = true;
            Cursor.visible = false;
            /*Cursor.lockState = CursorLockMode.Locked;*/
            /*Invoke("Destroy", 2f);*/
        }
    }
    public void TriggerDialogue()
    {
        sight.enemyinSight = false;
        FindObjectOfType<DialogueManager>().StartMotuDialogue(Motudialogue);
    }
    private void Destroy()
    {
        
        Destroy(this.gameObject);
    }
}
