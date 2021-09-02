using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessSliderController : MonoBehaviour
{
    private Slider brightnessSlider;
    private PostProcessVolume volume;
    private ColorGrading colorGrading;

    // Start is called before the first frame update
    void Start()
    {
        brightnessSlider = GetComponent<Slider>();
        
        GameObject gameObject = GameObject.Find("BrightnessPPV");
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.sharedProfile.TryGetSettings(out colorGrading);

        brightnessSlider.value = colorGrading.brightness.value;

    }

    // Update is called once per frame
    void Update()
    {
        colorGrading.brightness.value = brightnessSlider.value;
    }
}
