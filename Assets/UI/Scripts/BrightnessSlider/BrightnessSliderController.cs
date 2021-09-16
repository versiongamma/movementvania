using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessSliderController : MonoBehaviour
{
    private Slider brightnessSlider;
    private ColorGrading colorGrading;
    public PostProcessProfile brightnessProfile;
    // Start is called before the first frame update

    //Alters the color grading effect in the postproccessprofile to change brightness
    void Start()
    {
        brightnessSlider = GetComponent<Slider>();
        
        
        brightnessProfile.TryGetSettings(out colorGrading);
        brightnessSlider.value = colorGrading.brightness.value;
    }

    // Update is called once per frame
    void Update()
    {
        colorGrading.brightness.value = brightnessSlider.value;
    }
}
