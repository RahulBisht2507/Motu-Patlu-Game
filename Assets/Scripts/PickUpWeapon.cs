using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]private Animator motu;
    [SerializeField]private List<GameObject> Weapon;
    bool Meele, Axe,Sword,Axeslot2,Axeslot3,Sword2,Sword3;
    public bool MeeleMode, AxeMode,SwordMode;
    [SerializeField] private Image AttackIcon;
    [SerializeField] private Sprite[] sprite;
    bool weapon;
    int slots = 0;
    bool slot2, slot3;
    public bool EquipAxe;
    void Awake()
    {
        AxeMode = false;
        MeeleMode = true;
        SwordMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            for(int i = 0; i < 2; i++)
            {
                Weapon[i].SetActive(false);
            }
            MeeleMode = true;
            AxeMode = false;
            SwordMode = false;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (slot2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Weapon[i].SetActive(false);
                }
                if (Axeslot2)
                {
                    Weapon[0].SetActive(true);
                    EquipAxe = true;
                    MeeleMode = false;
                    AxeMode = true;
                    SwordMode = false;
                    motu.SetBool("AxeEquip", true);

                }else if (Sword2)
                {
                    Weapon[1].SetActive(true);
                    AxeMode = false;
                    MeeleMode = false;
                    SwordMode = true;
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (slot3)
            {
                for (int i = 0; i < 2; i++)
                {
                    Weapon[i].SetActive(false);
                }
                if (Axeslot3)
                {
                    Weapon[0].SetActive(true);
                    AxeMode = true;
                    EquipAxe = true;
                    SwordMode = false;
                }
                else if(Sword3)
                {
                    Weapon[1].SetActive(true);
                    AxeMode = false;
                    MeeleMode = false;
                    SwordMode = true;
                }
            }
        }


        AssignSlots();
        Switch();
    }
    private void AssignSlots()
    {
        if (weapon)
        {
            slots++;
            if(slots == 1)
            {
                if (Axe)
                {
                    slot2 = true;
                    Axeslot2 = true;
                    Axe = false;
                    weapon = false;
                }
                else if(Sword)
                {
                    slot2 = true;
                    Sword2 = true;
                    Sword = false;
                    weapon = false;
                }

            }else if(slots == 2)
            {
                if (Axe)
                {
                    slot3 = true;
                    Axeslot3 = true;
                    weapon = false;
                    Axe = false;
                }
                else if (Sword)
                {
                    slot3 = true;
                    Sword3 = true;
                    weapon = false;
                    Sword = false;
                }
            }
        }
    }
    public void Switch()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Axe"))
        {
            /*Weapon[0].SetActive(true);*/
            weapon = true;
            Axe = true;

        }
        if(other.CompareTag("Sword"))
        {
 /*           Weapon[1].SetActive(true);
            Weapon[0].SetActive(false);*/
            weapon = true;
            Sword = true;
        }
            
    }

    public void Meelee()
    {
        for (int i = 0; i < 2; i++)
        {
            Weapon[i].SetActive(false);
        }
        MeeleMode = true;
        AxeMode = false;
        SwordMode = false;
        AttackIcon.sprite = sprite[0];
        
    }

    public void Arxe()
    {
        AttackIcon.sprite = sprite[1];
        if (slot2)
        {
            for (int i = 0; i < 2; i++)
            {
                Weapon[i].SetActive(false);
            }
            if (Axeslot2)
            {
                Weapon[0].SetActive(true);
                EquipAxe = true;
                MeeleMode = false;
                AxeMode = true;
                SwordMode = false;
                motu.SetBool("AxeEquip", true);

            }
            else if (Sword2)
            {
                Weapon[1].SetActive(true);
                AxeMode = false;
                MeeleMode = false;
                SwordMode = true;
            }
        }
    }

    public void Ssword()
    {

    }
}
