using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoSaveChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] fileEntries = Directory.GetFiles(Application.persistentDataPath);
        foreach (string path in fileEntries) {
            
            if (path.Contains("AutoSave") && path.EndsWith(".bin"))
            {
                if (GameObject.Find("LoadAutosave")) {
                    Button autoSaveLoadButton = GameObject.Find("LoadAutosave").GetComponent<Button>();
                    if (autoSaveLoadButton)
                    {
                        autoSaveLoadButton.interactable = true;
                    }
                }
            }
            
            if (path.Contains("SaveData") && path.EndsWith(".bin"))
            {
                if (GameObject.Find("LoadSave")) {
                    Button saveLoadButton = GameObject.Find("LoadSave").GetComponent<Button>();
                    if (saveLoadButton)
                    {
                        saveLoadButton.interactable = true;
                    }
                }
            }
        }
    }

    void Update() 
    {
        Start();
    }
}
