using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer mainAudioMixer;

    public AudioMixer musicAudioMixer;

    public Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    public GameObject MainMenu;
    public GameObject OptionMenu;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> resolutionsList = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolution = resolutions[i].width + " x " + resolutions[i].height;
            resolutionsList.Add(resolution);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(resolutionsList);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

    }
    public void SetMainVolume(float volume)
    {
        mainAudioMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex) // 0 = Low, 1 = Medium, 2 = High
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ToggleOptionMenu()
    {
        if(MainMenu.activeInHierarchy)
        {
            MainMenu.SetActive(false);
            OptionMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            OptionMenu.SetActive(false);
        }
    }
}
