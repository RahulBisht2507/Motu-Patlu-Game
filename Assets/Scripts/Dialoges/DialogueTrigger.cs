using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public GameObject press2talk;
    public GameObject dilag;
    public PlayerMovement player;
    public GameObject next;
    public GameObject finalquest;
    public DialogueManager dialogueManager;


    public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
		{
            press2talk.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == ("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                press2talk.SetActive(false);
                player.enabled = false;
                dilag.SetActive(true);
                next.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                TriggerDialogue();

            }
            if (dialogueManager.endDialohue)
            {
                press2talk.SetActive(true);
                player.enabled = true;
                dilag.SetActive(false);
                next.SetActive(false);
             /*   Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;*/
                /*finalquest.SetActive(true);*/
            }
        }
    }
    public void Talk()
    {
        press2talk.SetActive(false);
        player.enabled = false;
        dilag.SetActive(true);
        next.SetActive(true);
        finalquest.SetActive(true);
      /*  Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
        TriggerDialogue();
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == ("Player"))
        {
            dialogueManager.endDialohue = false;
            press2talk.SetActive(false);
           
        }
    }
}
