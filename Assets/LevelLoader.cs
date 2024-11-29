using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainMenu;
    public Slider slider;
    
    public void LoadLevel(string scenename)
    {
        StartCoroutine(LoadAsyncniously(scenename));
        loadingScreen.SetActive(true);
        mainMenu.SetActive(false);
    }

    IEnumerator LoadAsyncniously(string scenename)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
