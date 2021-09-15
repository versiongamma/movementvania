using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenuOpener : MonoBehaviour
{
    [SerializeField] public GameObject mapFullUI;

    // Functions to open and hide map 
    public void OpenPanel()
    {
        if (mapFullUI != null)
        {
            mapFullUI.SetActive(true);
        }
    }

    public void HidePanel()
    {
        if (mapFullUI != null)
        {
            mapFullUI.SetActive(false);
        }
    }
}