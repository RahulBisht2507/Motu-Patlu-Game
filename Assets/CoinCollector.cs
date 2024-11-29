using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private PLayerMoney playerMoney;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            playerMoney.MoneyValue += Random.RandomRange(100, 130);
        }
    }
}
