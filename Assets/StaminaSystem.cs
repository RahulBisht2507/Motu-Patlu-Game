using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float Stamina = 100;
    public Slider StaminaBar;
    public PlayerMovement Player;
    public bool CanRun,refill;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.stamina == true)
        {
            if (Stamina > 0)
            {
                refill = false;
                Stamina -= 0.1f;
                StaminaBar.value = Stamina;
            }
        }
        else
        {
            Invoke("RefillStamina", 3f);
        }

        if (Stamina > 1)
        {
            CanRun = true;
        }else if (Stamina < 1)
        {
            CanRun = false;
        }

        if (refill)
        {
            if (Stamina < 100)
            {
                Stamina += 0.05f;
                StaminaBar.value = Stamina;
            }
        }
    }
    private void RefillStamina()
    {
        refill = true;
    }
}
