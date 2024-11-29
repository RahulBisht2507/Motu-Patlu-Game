using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterScript : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem death;
    public AudioSource explode;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            Invoke("Destroy", 0f);
            text.SetActive(true);
            Invoke("Restart", 3f);
        }
    }
    private void Destroy()
    {
        Destroy(player);
        death.Play();
        explode.Play();
    }
    private void Restart()
    {
        SceneManager.LoadScene("Motu");
    }
}
