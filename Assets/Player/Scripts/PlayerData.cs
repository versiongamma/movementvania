using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    // Class variables
    public Vector2 playerPosition;
    public Vector3 cameraPosition;
    public float[] cameraMinMax;
    public int playerHealth;
    public bool[] playerPowerups;
    public bool translateX;
    public bool translateY;
    public string activeSceneName;
    public string saveFileName;

    /*
     * Retrives relevant data from different objects in order to populate ourselves with data to be saved
     */
    public void popluateData(string fileName) {
        Debug.Log("Saving game...");
        if (fileName == null)
            fileName = "SaveData";
        this.saveFileName = fileName;
        GameObject player = GameObject.Find("Player");
        if (player == null)
            // Error here!
            Debug.Log("Null pointer!!");
        // Get players current data
        this.playerPosition = player.GetComponent<PlayerMovement>().getPlayerPosition();
        this.playerHealth = player.GetComponent<PlayerHealth>().getPlayerHealth();
        this.playerPowerups = player.GetComponent<PlayerEquipment>().getPowerUps();
        // Get the current levels file name for loading
        this.activeSceneName = SceneManager.GetActiveScene().name;
        // Get camera position data for the players current room
        this.cameraMinMax = GameObject.Find("Main Camera").GetComponent<CameraMovement>().getCameraMinMax();
        this.cameraPosition = Camera.main.transform.position;
        // Get information on whether or not the camera can move in the X or Y direction, as the player moves
        this.translateX = GameObject.Find("Main Camera").GetComponent<CameraMovement>().getCameraTranslateX();
        this.translateY = GameObject.Find("Main Camera").GetComponent<CameraMovement>().getCameraTranslateY();

        if (this.playerPosition != null && this.playerHealth != null && this.playerPowerups != null) // Ensure that we don't attempt to save NULL data
        {
            // Call the save function
            SaveLoad.saveData(this);
        }
    }

    /*
     * Retrives loaded data from the SaveLoad class and populates ourselves with said data 
     */
    public void loadPlayerData() 
    {
        SaveLoad.loadData();

        this.playerHealth = SaveLoad.health;
        this.playerPosition = new Vector2(SaveLoad.position[0], SaveLoad.position[1]);
        this.playerPowerups = SaveLoad.powerups;
        this.activeSceneName = SaveLoad.activeSceneName;
        this.cameraMinMax = SaveLoad.cameraMinMax;
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        if (pauseMenu == null) {
            // If pauseMenu is NULL we are loading from the start menu so we will need to load the given scene
            SceneManager.LoadScene(this.activeSceneName, LoadSceneMode.Single);
            return;
        }
        pauseMenu.GetComponent<PauseMenuUIToggle>().HidePanel();
        PauseMenu pm = GameObject.FindObjectOfType(typeof(PauseMenu)) as PauseMenu;
        pm.setPaused(false);
        pm.ResumeGame();
    }
}
