using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenImage : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject CloseIcon;
    [SerializeField] private GameObject image;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator quest;
    [SerializeField] private Animator questName;
    public bool QuestUpdate;
    public int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            text.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {

            /*if (Input.GetKey(KeyCode.E))
            {
                image.SetActive(true);
                player.enabled = false;
                text.SetActive(false);
                quest.SetBool("appear", false);
                questName.SetBool("Appear", false);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                count++;
                image.SetActive(false);
                player.enabled = true;
                *//*text.SetActive(true);*//*
                Destroy(this.gameObject);
                QuestUpdate = true;
                
            }*/
        }
     }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            text.SetActive(false);
        }
    }

    public void OpenClue()
    {
        image.SetActive(true);
        player.enabled = false;
        text.SetActive(false);
        quest.SetBool("appear", false);
        questName.SetBool("Appear", false);
        CloseIcon.SetActive(true);
    }

    public void CloseClue()
    {
        count++;
        image.SetActive(false);
        player.enabled = true;
        /*text.SetActive(true);*/
        Destroy(this.gameObject);
        QuestUpdate = true;
        CloseIcon.SetActive(false);
    }
}
