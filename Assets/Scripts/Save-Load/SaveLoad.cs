using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoad
{
    // Class variables
    public static bool loaded = false;
    public static string activeSceneName;
    public static float[] position;
    public static float[] cameraPosition;
    public static float[] cameraMinMax;
    public static bool translateX;
    public static bool translateY;
    public static int health;
    public static bool[] powerups;
    public static Dictionary<string, string[]> minimapExplored;

    /*
     * Write given PlayerData data to disk for loading at a later time
     */
    public static void saveData(PlayerData pd) 
    {
        // Convert the given PlayerData object into a SaveData object for writing to disk
        SaveData save = new SaveData();

        save.activeSceneName = pd.activeSceneName;

        save.position = new float[2];
        save.position[0] = pd.playerPosition.x;
        save.position[1] = pd.playerPosition.y;

        save.health = pd.playerHealth;

        save.powerups = pd.playerPowerups;

        save.cameraPosition = new float[3];
        save.cameraPosition[0] = pd.cameraPosition.x;
        save.cameraPosition[1] = pd.cameraPosition.y;
        save.cameraPosition[2] = pd.cameraPosition.z;

        save.cameraMinMax = new float[4];
        save.cameraMinMax[0] = pd.cameraMinMax[0];
        save.cameraMinMax[1] = pd.cameraMinMax[1];
        save.cameraMinMax[2] = pd.cameraMinMax[2];
        save.cameraMinMax[3] = pd.cameraMinMax[3];

        save.translateX = pd.translateX;
        save.translateY = pd.translateY;

        int arrayLength = 0;

        for (int i = 0; i < pd.minimapExplored.Values.Count; i++) 
        {
            for (int k = 0; k < pd.minimapExplored.Values.ElementAt(i).Count(); k++) 
            {
                arrayLength++;
            }
        }

        save.minimapExploredSceneName = pd.minimapExplored.Keys.ToArray<string>();
        save.minimapExplored = new string[arrayLength];
        save.minimapExploredIndex = new int[arrayLength];

        for (int i = 0; i < pd.minimapExplored.Keys.Count; i++) 
        {
            for (int k = 0; k < pd.minimapExplored.Values.ElementAt(i).Count(); k++) 
            {
                save.minimapExploredIndex[k] = Array.IndexOf(save.minimapExploredSceneName, pd.minimapExplored.Keys.ElementAt(i));
                save.minimapExplored[k] = pd.minimapExplored.Values.ElementAt(i)[k];
            }
        }

        // Writes save data to the 'AppData' folder on Windows, not sure about MacOS
        string savePath = Application.persistentDataPath + "/" + pd.saveFileName + ".bin";
        // Convert SaveData object to JSON data for writing
        string jsonData = JsonUtility.ToJson(save, true);
        File.WriteAllText(savePath, jsonData);

    }

    public static void saveDataLevelChange(PlayerData pd) 
    {
        // Convert the given PlayerData object into a SaveData object for writing to disk
        SaveData save = new SaveData();

        save.health = pd.playerHealth;

        save.powerups = pd.playerPowerups;

        int arrayLength = 0;

        for (int i = 0; i < pd.minimapExplored.Values.Count; i++) 
        {
            for (int k = 0; k < pd.minimapExplored.Values.ElementAt(i).Count(); k++) 
            {
                arrayLength++;
            }
        }

        save.minimapExploredSceneName = pd.minimapExplored.Keys.ToArray<string>();
        save.minimapExplored = new string[arrayLength];
        save.minimapExploredIndex = new int[arrayLength];

        for (int i = 0; i < pd.minimapExplored.Keys.Count; i++) 
        {
            for (int k = 0; k < pd.minimapExplored.Values.ElementAt(i).Count(); k++) 
            {
                save.minimapExploredIndex[k] = Array.IndexOf(save.minimapExploredSceneName, pd.minimapExplored.Keys.ElementAt(i));
                save.minimapExplored[k] = pd.minimapExplored.Values.ElementAt(i)[k];
            }
        }

        // Writes save data to the 'AppData' folder on Windows, not sure about MacOS
        string savePath = Application.persistentDataPath + "/" + pd.saveFileName + ".bin";
        // Convert SaveData object to JSON data for writing
        string jsonData = JsonUtility.ToJson(save, true);
        File.WriteAllText(savePath, jsonData);

    }

    /*
     * Load data from a JSON file on disk and populate ourselves with said data
     */
    public static void loadData(String filename = "SaveData") 
    {
        clear();

        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/" + filename + ".bin"));

        activeSceneName = save.activeSceneName;
        position = new float[2];
        position[0] = save.position[0];
        position[1] = save.position[1];

        cameraPosition = new float[3];
        cameraPosition[0] = save.cameraPosition[0];
        cameraPosition[1] = save.cameraPosition[1];
        cameraPosition[2] = save.cameraPosition[2];

        cameraMinMax = new float[4];
        cameraMinMax[0] = save.cameraMinMax[0];
        cameraMinMax[1] = save.cameraMinMax[1];
        cameraMinMax[2] = save.cameraMinMax[2];
        cameraMinMax[3] = save.cameraMinMax[3];

        translateX = save.translateX;
        translateY = save.translateY;

        health = save.health;

        powerups = save.powerups;

        minimapExplored = new Dictionary<string, string[]>();
        for (int k = 0; k < save.minimapExploredSceneName.Count(); k++) 
        {
            int arrayLength = 0;
            for (int i = 0; i < save.minimapExplored[k].Count(); i++) 
            {
                arrayLength++;
            }
            string[] tempArray = new string[arrayLength];
            for (int i = 0; i < save.minimapExplored.Count(); i++) 
            {
                tempArray[i] = save.minimapExplored.ElementAt(i);
            }
            minimapExplored.Add(save.minimapExploredSceneName.ElementAt(k), tempArray);
        }
        loaded = true;
    }

    public static void loadDataLevelChange(String filename) 
    {
        clear();

        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/" + filename + ".bin"));

        health = save.health;

        powerups = save.powerups;

        minimapExplored = new Dictionary<string, string[]>();
        for (int k = 0; k < save.minimapExploredSceneName.Count(); k++) 
        {
            int arrayLength = 0;
            for (int i = 0; i < save.minimapExplored[k].Count(); i++) 
            {
                arrayLength++;
            }
            string[] tempArray = new string[arrayLength];
            for (int i = 0; i < save.minimapExplored.Count(); i++) 
            {
                tempArray[i] = save.minimapExplored.ElementAt(i);
            }
            minimapExplored.Add(save.minimapExploredSceneName.ElementAt(k), tempArray);
        }
        loaded = true;
    }
    /*
     * Null out all current data, to ensure that no leftover data is present 
     */
    public static void clear() 
    {
        activeSceneName = null;
        position = null;
        cameraPosition = null;
        cameraMinMax = null;
        health = new int();
        powerups = null;
    }
}
