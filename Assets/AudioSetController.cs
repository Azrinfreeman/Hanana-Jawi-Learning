using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public TextMeshProUGUI text;

    public bool isSpeech;
    public bool isMusic;
    public bool isMaster;

    void Update()
    {
        text.text = (slider.value * 100).ToString("00");
    }

    void Start()
    {
        text = transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
        if (isMusic)
        {
            if (PlayerPrefs.GetFloat("MusicVolume") == 0)
            {
                slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
        else if (isMaster)
        {
            if (PlayerPrefs.GetFloat("MasterVolume") == 0)
            {
                slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("MasterVolume");
            }
        }
        else if (isSpeech)
        {
            if (PlayerPrefs.GetFloat("SpeechVolume") == 0)
            {
                slider.value = PlayerPrefs.GetFloat("SpeechVolume", 0.75f);
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("SpeechVolume");
            }
        }
        else
        {
            if (PlayerPrefs.GetFloat("SFXVolume") == 0)
            {
                slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("SFXVolume");
            }
        }
    }

    public void SetLevelMaster()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetLevelMusic()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetLevelSpeech()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("SpeechVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SpeechVolumeSpeechVolume", sliderValue);
    }

    public void SetLevelSFX()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }
}
