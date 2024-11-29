using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
    public bool dealdamage,ithit;
    public LayerMask layerMask;
    public CineShake cam;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    void Start()    
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.up, out hit, weaponLength, layerMask))
            {
            ithit = true;
            cam.ShakeCam(2f, .5f);
                if (hit.transform.TryGetComponent(out AiEnemy health) && !dealdamage)
                {
                    health.skelton.SetTrigger("hit");
                    health.blood.Play();
                    health.takeDamage(weaponDamage);
                    hasDealtDamage = true;
                    dealdamage = true;
                }else if (hit.transform.TryGetComponent(out Mushroom enemy1) && !dealdamage)
                {
                    enemy1.takeDamage(1);
                    enemy1.hit.Play();
                    hasDealtDamage = true;
                    dealdamage = true;
                }
        }
        else
        {
            dealdamage = false;
            ithit = false;
        }

    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
    private void OnDisable()
    {
        
    }
    private void OnEnable()
    {
        
    }
}