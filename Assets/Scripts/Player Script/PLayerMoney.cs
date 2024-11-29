using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLayerMoney : MonoBehaviour
{
    public Text MoneyUi;
    public float MoneyValue = 1000;

    void Update()
    {
        MoneyUi.text = MoneyValue.ToString();
    }

    public void buyItem(float ItemValue)
    {
        if (MoneyValue > 0)
        {
            MoneyValue -= ItemValue;
        }
    }
}
