using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData
{
    public string activeSceneName;
    public float[] position;
    public float[] cameraPosition;
    public float[] cameraMinMax;
    public int health;
    public bool[] powerups;
    public CameraBoundsHandler startingBounds;
}