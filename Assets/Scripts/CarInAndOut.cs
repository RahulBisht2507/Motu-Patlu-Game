using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInAndOut : MonoBehaviour
{
    public GameObject OpenCar;
    public GameObject motu;
    public CarController car;
    public Camera main;
    public Camera carCamera;
    public AirPlaneController plane;
    public Rigidbody carBody;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            OpenCar.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == ("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                motu.SetActive(false);
                car.enabled = true;
                main.enabled = false;
                carCamera.enabled = true;
                OpenCar.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            plane.enabled = true;
            car.enabled = false;
            carBody.useGravity = false;
        }
    }
}
