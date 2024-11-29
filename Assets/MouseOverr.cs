using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverr : MonoBehaviour
{
    public Image ui;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseOver()
    {
        ui.color = Color.blue;
    }
    private void OnMouseExit()
    {
        ui.color = Color.clear;
    }
}
