using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    /*public GameObject can;
    public Animator press2buy;*/
    public GameObject shoppanel;
    public GameObject shopicon;
    public PLayerMoney money;
    public float count = 0;
    public float ujkacount = 0;
    public float speedcount = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI ujkaText;
    public GameObject ShopMenu;
    public GameObject SamosaItem;
    public GameObject backbutton;
    [SerializeField] private PLayerMoney wallet;
    

    // Update is called once per frame
    void Update()
    {
        countText.text = count.ToString();
        speedText.text = speedcount.ToString();
        ujkaText.text = ujkacount.ToString();

    }
    public void Samosa()
    {
        if(wallet.MoneyValue >0)
        {
            float samosaPrice = 500;
            money.buyItem(samosaPrice);
            count++;
        }
        
    }
    public void giantSamosa()
    {
        if(wallet.MoneyValue >0)
        {
            float giantSamosaPrice = 1000;
            money.buyItem(giantSamosaPrice);
        }
      
    }

    public void speedSamosa()
    {
        if(wallet.MoneyValue >0)
        {
            float speedSamosaPrice = 500;
            money.buyItem(speedSamosaPrice);
            speedcount++;

        }
       
    }

    public void UjakSamosa()
    {
        if (wallet.MoneyValue > 0)
        {

            float ujakSamosaPrice = 500;
            money.buyItem(ujakSamosaPrice);
            ujkacount++;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            /* press2buy.Play("Press2Buy");
             can.SetActive(true);*/
            shopicon.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == ("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                /*can.SetActive(false);*/
                
            }
           
        }
    }
    public void Shop()
    {
        shoppanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == ("Player"))
        {
            /* can.SetActive(false);*/
            shopicon.SetActive(false);
        }
    }
   public void onPressedShop()
    {
        backbutton.SetActive(true);
        ShopMenu.SetActive(false);
        SamosaItem.SetActive(true);
    }
    
  public void Back()
    {
        ShopMenu.SetActive(true);
        SamosaItem.SetActive(false);
        backbutton.SetActive(false);
    }
    public void Exit()
    {
        shoppanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
