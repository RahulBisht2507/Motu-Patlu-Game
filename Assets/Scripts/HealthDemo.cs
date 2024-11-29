using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthDemo : MonoBehaviour
{
    public GameObject gameover;
    
    public Slider bar;
    public float health;
    public Animator motu;
 
    public GameObject respawn;
    public GameObject player;
    public ParticleSystem explode;
    public AudioSource ex;

    public ShopSystem shop;
    // Update is called once per frame
    void Update()
    {
       
        health = bar.value;
        heathbar();
    }
    public void heathbar()
    {
        bar.value = health;
    }
    public void addhealth()
    {
        if (shop.count == 0) return;
        if (health < 91)
        {
            health += 10;
            bar.value = health;
            shop.count--;
        }

    }
    public void subhealth(float damage)
    {
        if (health > 0)
        {
                health -= damage;
                bar.value = health;
        }else if(health <= 0)
        {
            motu.SetTrigger("Death");
         
            Invoke("kya", .5f);
            Invoke("Respawn", 2f);
        }
    }
    public void kya()
    {
        gameover.SetActive(true);
    }
    public void Respawn()
    {
        explode.Play();
        ex.Play();
        SceneManager.LoadScene("Motu");
    }
}
