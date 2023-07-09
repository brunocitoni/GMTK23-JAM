using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Dropdown resolutiondrop;
    public AudioMixer audioMixer;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutiondrop.ClearOptions();

        List<string> options = new List<string>();
        int currentresolutionindex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentresolutionindex = i;
            }
        }

        resolutiondrop.AddOptions(options);
        resolutiondrop.value = currentresolutionindex;
        resolutiondrop.RefreshShownValue();
    }

    public void Setreso(int resolutionindex)
    {
        Resolution res = resolutions[resolutionindex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
