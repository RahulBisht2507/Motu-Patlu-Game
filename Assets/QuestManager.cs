using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questTextName;
    public TextMeshProUGUI questText;
    public GameObject MainQuest;
    public Animator questName;
    public Animator quest;
   
    public PlayerMovement playerMovement;
    public EnterHouse house;
    public OpenImage openImage;
    public AudioSource sound;
    public BridgeUpdate bridge;
    public List<GameObject> QuestGameObject;
  
    public Quest1Upgrade FinalQuest;
    /*public float count = 0;*/
    
    void Awake()
    {
        Invoke("Quest1", 0f);
    }

    // Update is called once per frame
    void Update()
    {
       /* Debug.Log(count);*/
        if (house.done)
        {
            questTextName.text = "Find Some Clues".ToString();
            questText.text = "Quest Updated".ToString();
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName",1.5f);
           /* for(int i = 0; i < 2; i++)
            {
                QuestGameObject[i].SetActive(true);
            }*/
        }
        if (openImage.QuestUpdate)
        {
            questTextName.text = "Go To Bridge".ToString();
            questText.text = "Quest Updated".ToString();
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName", 1.5f);
            for (int i = 0; i < 2; i++)
            {
                QuestGameObject[i].SetActive(true);
            }
            /* count++;*/
        }

        if (bridge.bridgeUpdate)
        {
            questTextName.text = "Talk To DrJhatka".ToString();
            questText.text = "Quest Updated".ToString();
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName", 1.5f);
        }
        if (FinalQuest.questUpdate)
        {
            questTextName.text = "GO NEAR BIG GATE".ToString();
            questText.text = "Quest Updated".ToString();
            QuestGameObject[2].SetActive(true);
            Invoke("ResetQuest", 1f);
            Invoke("ResetQuestName", 1.5f);
        }

    }
    public void Quest1() 
    { 
        
        playerMovement.enabled = false;
        Invoke("Quest1ScreenEnd", 3f);
        Invoke("ResetQuest", 4f);
        Invoke("GoHOme", 4f);
    }
    private void Quest1ScreenEnd()
    {
        MainQuest.SetActive(false);
        playerMovement.enabled = true;
    }
    private void GoHOme()
    {
        questName.gameObject.SetActive(true);
        questName.SetBool("Appear", true);
       
    }
    private void FindClue()
    {
    }
    private void ResetQuest()
    {
        quest.gameObject.SetActive(true);
        quest.SetBool("appear", true);
        sound.Play();
        openImage.QuestUpdate = false;
    }
    private void ResetQuestName()
    {
        sound.Play();
        questName.gameObject.SetActive(true);
        questName.SetBool("Appear", true);
        house.done = false;
    }
}
