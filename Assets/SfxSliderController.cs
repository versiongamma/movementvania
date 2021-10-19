using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SfxSliderController : MonoBehaviour
{
    // Get the global audio mixer
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    //Initialize with previously used sfx volume
    public void Start()
    {
        if (PlayerPrefs.HasKey("SfxVolume")) {
            slider.value = PlayerPrefs.GetFloat("SfxVolume");
        } else {
            PlayerPrefs.SetFloat("SfxVolume", 1);
            slider.value = 1;
        }
    }
    //Function to translate slider value to volume for mixer group
    public void SetLevel(float SliderValue)
    {
        mixer.SetFloat("SfxVolume", Mathf.Log10(SliderValue) * 20);
        PlayerPrefs.SetFloat("SfxVolume", slider.value);
    }
}
