using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public List<GameObject> waypoints;
    public Animator motu;
    int index = 0;
    bool isLoop = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 2;

        Vector3 destination = waypoints[index].transform.position;
        Vector3 newpos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newpos;
        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.5)
        {
            if (index < waypoints.Count - 1)
            {
                index++;
            }
            else
            {
                if (isLoop)
                {
                    index = 0;
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            Debug.Log("gotten Hit");
            motu.SetTrigger("hit");
            motu.ResetTrigger("idle");
            motu.ResetTrigger("sprint");
            motu.ResetTrigger("run");
            motu.ResetTrigger("jump");

        }
    }
}
