using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    public int qualityIndex;
    public List<GameObject> HoverTick;
    public List<GameObject> NotHoverTick;
    public AudioMixer audiomixer;
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }
    public void Performant()
    {
        qualityIndex = 0;
        SetQuality(qualityIndex);
        for (int i = 0; i < 3; i++)
        {
            HoverTick[i].SetActive(false);
            NotHoverTick[i].SetActive(false);
        }
        HoverTick[0].SetActive(true);
        NotHoverTick[0].SetActive(true);
        
    }
    public void Balanced()
    {
        qualityIndex = 1;
        SetQuality(qualityIndex);
        for (int i = 0; i < 3; i++)
        {
            HoverTick[i].SetActive(false);
            NotHoverTick[i].SetActive(false);
        }
        HoverTick[1].SetActive(true);
        NotHoverTick[1].SetActive(true);
    }
    public void High()
    {
        qualityIndex = 2;
        SetQuality(qualityIndex);
        for (int i = 0; i < 3; i++)
        {
            HoverTick[i].SetActive(false);
            NotHoverTick[i].SetActive(false);
        }
        HoverTick[2].SetActive(true);
        NotHoverTick[2].SetActive(true);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }
    public void FullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
