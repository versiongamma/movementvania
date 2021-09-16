using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData
{
    // Class variables
    public string activeSceneName;
    public float[] position;
    public float[] cameraPosition;
    public float[] cameraMinMax;
    public bool translateX;
    public bool translateY;
    public int health;
    public bool[] powerups;
}