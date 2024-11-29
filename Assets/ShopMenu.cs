using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject nothover;
    public GameObject hover;

    public void enterButtonUi()
    {
        hover.SetActive(true);
        nothover.SetActive(false);
    }
   public void exitButtonUi()
    {
        nothover.SetActive(true);
        hover.SetActive(false);
    }
}
