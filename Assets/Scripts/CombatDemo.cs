using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDemo : MonoBehaviour
{
    public LayerMask layerMask;
   /* public CineShake cam;*/
    public PickUpWeapon weopon;
    public bool itHit;
    public bool dealdamagae;
    public bool candealDamage;
    bool hitt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            RaycastHit hit;
        if (weopon.MeeleMode)
        {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1.1f, layerMask))
                {
                itHit = true;
                    /* Debug.Log("It Hit Something");*/

                    if (hit.transform.TryGetComponent(out AiEnemy enemy) && !dealdamagae)
                    {
                        /*   Debug.Log("ithit");*/
                        enemy.takeDamage(1);
                        enemy.blood.Play();
                        hitt = false;
                        dealdamagae = true;

                    }
                    else if (hit.transform.TryGetComponent(out Mushroom enemy1) && !dealdamagae)
                    {
                        enemy1.takeDamage(1);
                        enemy1.hit.Play();
                        hitt = false;
                        dealdamagae = true;
                    }else if(hit.transform.TryGetComponent(out BossEmemy ghasita) && !dealdamagae)
                    {
                        ghasita.takeDamage(1);
                        hitt = false;
                        dealdamagae = true;
                }

                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1f, Color.green);
                    itHit = false;
                    dealdamagae = false;
                }
        }
        
    }
    
}
