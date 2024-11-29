using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider bar;
    float health = 3f;

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
        if (health < 3)
        {
            health += 10;
            bar.value = health;
        }

    }
    public void subhealth(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            bar.value = health;
        }
        else if (health <= 0)
        {

        }
    }
}
