using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public AudioSource loopTime;
    public AudioSource forest;
    void Start()
    {
        Invoke("Loop", 5f);
    }

    private void Loop()
    {
        loopTime.Play();
        forest.Stop();
        Invoke("Forest", 180f);
    }
    private void Forest()
    {
        forest.Play();
        loopTime.Stop();
        Invoke("Loop", 180f);
    }

}
