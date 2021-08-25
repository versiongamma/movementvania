using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSliderController : MonoBehaviour
{
    private Slider brightnessSlider;

    // Start is called before the first frame update
    void Start()
    {
        brightnessSlider = GetComponent<Slider>();
        brightnessSlider.value = 0;

        GameObject volume = GameObject.Find("BrightnessPPV");
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
