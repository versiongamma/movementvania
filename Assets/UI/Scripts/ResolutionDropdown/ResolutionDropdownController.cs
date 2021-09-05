using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdownController : MonoBehaviour
{
    public Dropdown resDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> dropdownOptions = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string res = resolutions[i].width + " x " + resolutions[i].height + 
                         " " + resolutions[i].refreshRate + "Hz";
            dropdownOptions.Add(res);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }

        resDropdown.AddOptions(dropdownOptions);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();
    }

    public void setRes(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
