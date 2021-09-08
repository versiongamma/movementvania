using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFull : MonoBehaviour
{
    [SerializeField] public GameObject mapFullUI;
    [SerializeField] private bool viewMap;

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

    void MapView()
    {
        mapFullUI.SetActive(true);
    }

    void MapHide()
    {
        mapFullUI.SetActive(false);
    }
}