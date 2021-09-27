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
                Button autoSaveLoadButton = GameObject.Find("LoadAutosave").GetComponent<Button>();
                if (autoSaveLoadButton)
                {
                    autoSaveLoadButton.interactable = true;
                }
            }
            if (path.Contains("SaveData") && path.EndsWith(".bin"))
            {
                Button saveLoadButton = GameObject.Find("LoadSave").GetComponent<Button>();
                if (saveLoadButton)
                {
                    saveLoadButton.interactable = true;
                }
            }
        }
    }

    void Update() 
    {
        Start();
    }
}
