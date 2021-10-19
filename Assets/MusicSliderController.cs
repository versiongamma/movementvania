using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSliderController : MonoBehaviour
{
    // Get the global audio mixer
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    //Initialize with previously used sfx volume
    public void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume")) {
            slider.value = PlayerPrefs.GetFloat("MusicVolume");
        } else {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            slider.value = 1;
        }
        
    }
    //Function to translate slider value to volume for mixer group
    public void SetLevel(float SliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(SliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }
}
