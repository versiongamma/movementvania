using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoad
{
    public static bool loaded = false;
    public static string activeSceneName;
    public static float[] position;
    public static float[] cameraPosition;
    public static float[] cameraMinMax;
    public static int health;
    public static bool[] powerups;
    public static CameraBoundsHandler startingBounds;

    public static void saveData(PlayerData pd) 
    {
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

        save.startingBounds = pd.startingBounds;

        string savePath = Application.persistentDataPath + "/SaveData.bin";
        Debug.Log(savePath);
        string jsonData = JsonUtility.ToJson(save, true);

        File.WriteAllText(savePath, jsonData);

    }

    public static void loadData() 
    {
        clear();

        SaveData save = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/SaveData.bin"));

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

        startingBounds = save.startingBounds;

        health = save.health;

        powerups = save.powerups;

        loaded = true;
    }

    public static void clear() 
    {
        activeSceneName = null;
        position = null;
        cameraPosition = null;
        cameraMinMax = null;
        health = new int();
        powerups = null;
        startingBounds = null;
    }
}
