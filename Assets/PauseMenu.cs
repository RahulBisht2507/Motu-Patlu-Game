using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    
    public PlayerMovement player;
    public GameObject quit;
    bool pause;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            /*Cursor.lockState = CursorLockMode.Confined;*/
            Pause();
        }
        if (pause)
        {
            Time.timeScale = 0f;
        }
    }
    public void Pause()
    {
        pause = true;
        player.enabled = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pause = false;
        player.enabled = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void quu()
    {
        quit.SetActive(true);
    }
    public void fno()
    {
        quit.SetActive(false);
    }
  
   
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
