using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    // Function to set the sliders max health
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Function to modify the slliders values 
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
