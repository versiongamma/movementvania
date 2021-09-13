using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public Vector2 playerPosition;
    public Vector3 cameraPosition;
    public float[] cameraMinMax;
    public int playerHealth;
    public bool[] playerPowerups;
    public bool translateX;
    public bool translateY;
    public string activeSceneName;

    public PlayerData()
    {
    }

    public void popluateData() {
        GameObject player = GameObject.Find("Player");
        if (player == null)
            // Error here!
            Debug.Log("Null pointer!!");
        this.playerPosition = player.GetComponent<PlayerMovement>().getPlayerPosition();
        this.playerHealth = player.GetComponent<PlayerHealth>().getPlayerHealth();
        this.playerPowerups = player.GetComponent<PlayerEquipment>().getPowerUps();
        this.activeSceneName = SceneManager.GetActiveScene().name;
        this.cameraMinMax = GameObject.Find("Main Camera").GetComponent<CameraMovement>().getCameraMinMax();
        this.cameraPosition = Camera.main.transform.position;
        this.translateX = GameObject.Find("Main Camera").GetComponent<CameraMovement>().getCameraTranslateX();
        this.translateY = GameObject.Find("Main Camera").GetComponent<CameraMovement>().getCameraTranslateY();

        if (this.playerPosition != null && this.playerHealth != null && this.playerPowerups != null)
        {
            SaveLoad.saveData(this);
        }
    }

    public void loadPlayerData() 
    {
        SaveLoad.loadData();

        this.playerHealth = SaveLoad.health;
        this.playerPosition = new Vector2(SaveLoad.position[0], SaveLoad.position[1]);
        this.playerPowerups = SaveLoad.powerups;
        this.activeSceneName = SaveLoad.activeSceneName;
        this.cameraMinMax = SaveLoad.cameraMinMax;
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.GetComponent<PauseMenuUIToggle>().HidePanel();
        PauseMenu pm = GameObject.FindObjectOfType(typeof(PauseMenu)) as PauseMenu;
        pm.setPaused(false);
        pm.ResumeGame();
    }
}
