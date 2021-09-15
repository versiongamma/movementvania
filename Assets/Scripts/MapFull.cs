using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFull : MonoBehaviour
{
    [SerializeField] public GameObject mapFullUI;
    [SerializeField] public bool viewMap;

    // Checks for if user has pressed M key to open map
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            viewMap = !viewMap;

            if (viewMap)
            {
                MapView();
            }
            else
            {
                MapHide();
            }
        }
    }

    // Show map
    public void MapView()
    {
        mapFullUI.SetActive(true);
    }

    // Hide map
    public void MapHide()
    {
        mapFullUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Used to update the local variable if map is hidden from another function
    public void updateViewMap()
    {
        viewMap = !viewMap;
    }
}