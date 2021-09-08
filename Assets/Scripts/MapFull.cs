using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFull : MonoBehaviour
{
    [SerializeField] public GameObject mapFullUI;
    [SerializeField] public bool viewMap;

    // Update is called once per frame
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

    public void MapView()
    {
        mapFullUI.SetActive(true);
    }

    public void MapHide()
    {
        mapFullUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void updateViewMap()
    {
        viewMap = !viewMap;
    }
}