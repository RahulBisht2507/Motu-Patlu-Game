using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamosaTimeChecker : MonoBehaviour
{
    public Text Retime;
    public int count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Retime.text = count.ToString();
        
    }
    public void CountDown()
    {
        count--;
    }
}
