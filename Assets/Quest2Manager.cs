using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest2Manager : MonoBehaviour
{
    public GameObject dialgue1;
    public GameObject dial2;
    public QuestManagerr Quest;
    public TextMeshProUGUI questTextName;
    public TextMeshProUGUI questText;
    public GameObject dialogue;
    /*public GameObject dialugue2;*/
    public FInalQuest1 NOMonster;
    public Quest2Upgrade forestquest;
    public Animator questName;
    public Animator quest;
    public FInalQuest1 ForestComplete;
    public List<GameObject> AllQuest2;
    public List<Quest2Upgrade> AllQuestUpdate;
    bool forquestflow;
    private void Update()
    {
        if (Quest.Quest1.quest1End)
        {
            questText.text = "Quest Updated".ToString();
            questTextName.text = "Talk To Dr.Jhatka".ToString();
            

            /*     dialugue2.SetActive(false);*/
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName", 1.5f);
            /*Quest.Quest1.quest1End = false;*/
        }
        if (NOMonster != null)
        {
            if (NOMonster.enemyinSight == false && forquestflow == false)
            {
                questText.text = "Quest Updated".ToString();
                questTextName.text = "Talk To Dr.Jhatka".ToString();
                Invoke("ResetQuest", 1f);
                Invoke("ResetQuestName", 1.5f);
            }
        }

        if (forestquest.questUpdate)
        {
            questText.text = "Quest Updated".ToString();
            questTextName.text = "Go to Forest".ToString();
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName", 1.5f);
        }
        if (ForestComplete.enemyinSight == false)
        {
            forquestflow = true;
            questText.text = "Quest Updated".ToString();
            questTextName.text = "Talk To Dr.Jhatka".ToString();
           /* dialugue2.SetActive(true);*/
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName", 1.5f);
            /*Destroy(dialgue1);*/
            /*dialogue.SetActive(false);
            dialgue1.SetActive(false);
            dial2.SetActive(true);*/
        }
      /*  if (AllQuestUpdate[0].questUpdate)
        {
            if (AllQuestUpdate[0] != null)
            {
                AllQuest2[0].SetActive(true);
            }
            
        }*/
        if (AllQuestUpdate[2].questUpdate)
        {
            AllQuest2[1].SetActive(true);
            AllQuest2[2].SetActive(true);
        }
       

    }
    private void ResetQuest()
    {
        quest.gameObject.SetActive(true);
        quest.SetBool("appear", true);
        /*sound.Play();
        openImage.QuestUpdate = false;*/
    }
    private void ResetQuestName()
    {
        /*sound.Play();*/
        questName.gameObject.SetActive(true);
        questName.SetBool("Appear", true);
        /*house.done = false;*/
    }
}

