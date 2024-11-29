using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerr : MonoBehaviour
{
    public Quest1End Quest1;
    public GameObject Quest2;
    public List<GameObject> AllQuest;
    
    private void Update()
    {
        if (Quest1.quest1End)
        {
            Destroy(AllQuest[0]);
            Destroy(AllQuest[1]);
            Quest2.SetActive(true);
        }


    }
}
