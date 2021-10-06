using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxSliderController : MonoBehaviour
{
    // Get the global audio mixer
    [SerializeField] AudioMixer mixer;


    //Function to translate slider value to volume for mixer group
    public void SetLevel(float SliderValue)
    {
        mixer.SetFloat("SfxVolume", Mathf.Log10(SliderValue) * 20);
    }
}
