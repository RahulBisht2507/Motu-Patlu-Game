using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public GameObject controls;
    public GameObject credits;
    private void Start()
    {
        /*Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
    }
    public void Play()
    {
        SceneManager.LoadScene("Motu");
    }

    public void Control()
    {
        controls.SetActive(true);
    }
    public void Credit()
    {
        credits.SetActive(true);
    }
    public void back()
    {
        controls.SetActive(false);
        credits.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
