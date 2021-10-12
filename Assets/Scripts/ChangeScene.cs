using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



//This script is used to load in a new scene 
public class ChangeScene : MonoBehaviour
{
    //the scene name is passed through into the function and then loaded
    //using SceneManager
    public void LoadScene(string sceneName)
    {
        if (string.Compare(SceneManager.GetActiveScene().name, "StartMenu") == 0 && string.Compare(sceneName, "SettingsMenu") == 0) 
        {
            File.Create(Application.persistentDataPath + "/startMenuTempFile").Dispose();
        }
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAndSave(string sceneName)
    {
        try {
            if (System.IO.File.Exists(Application.persistentDataPath + "/startMenuTempFile")) 
            {
                File.Delete(Application.persistentDataPath + "/startMenuTempFile");
                SceneManager.LoadScene("StartMenu");
                return;
            }
            // Save temp data for loading after closing settings menu
            new PlayerData().popluateData("TempSaveData");
            SceneManager.LoadScene(sceneName);
        } catch (Exception e) {
        }
    }
}
