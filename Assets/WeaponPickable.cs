using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickable : MonoBehaviour
{
    [SerializeField] private Image axelogo;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1, 1, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            axelogo.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
