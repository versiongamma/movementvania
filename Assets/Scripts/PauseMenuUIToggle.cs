using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUIToggle : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuUI;

    public void HidePanel()
    {
        pauseMenuUI.SetActive(false);
    }

    public void ShowPanel()
    {
        pauseMenuUI.SetActive(true);
    }
}