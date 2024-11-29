using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingMan : MonoBehaviour
{
    public Animator dancingMan;
    private void Awake()
    {
        Dance1();
    }
    private void Dance1()
    {
        dancingMan.SetBool("Dance", false);
        Invoke("Dance2", 15f);
    }
    private void Dance2()
    {
        dancingMan.SetBool("Dance", true);
        Invoke("Dance1", 15f);
    }
}